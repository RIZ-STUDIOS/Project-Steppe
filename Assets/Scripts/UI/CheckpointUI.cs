using Cinemachine;
using ProjectSteppe.Currencies;
using ProjectSteppe.Entities.Player;
using ProjectSteppe.Interactions.Interactables;
using ProjectSteppe.Saving;
using ProjectSteppe.UI.Menus;
using ProjectSteppe.ZedExtensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace ProjectSteppe.UI
{
    public class CheckpointUI : MenuBase
    {
        private CheckpointInteractable checkpoint;

        [SerializeField]
        private GameObject firstButton;

        [SerializeField]
        private GameObject restartButton;

        [SerializeField]
        private CanvasGroup levelUpCG;

        [SerializeField]
        private GameObject levelUpFirstButton;

        [SerializeField]
        private float fadeSpeed = 1f;

        [SerializeField]
        private TextMeshProUGUI costTMP;

        [SerializeField]
        private TextMeshProUGUI currentExpTMP;

        private int pointsCost;

        private List<LevelUpButton> levelUpButtons = new();

        private int currentCost;

        private int CurrentExperience => checkpoint.player.CurrencyContainer.GetCurrencyAmount(CurrencyType.Experience);

        protected override void Awake()
        {
            base.Awake();
            levelUpButtons = GetComponentsInChildren<LevelUpButton>().ToList();
        }

        private void SubscribeToLevelUpButtons()
        {
            foreach (var button in levelUpButtons)
            {
                button.ActivateButtons();
                button.OnValueChange.AddListener(RefreshCost);
            }
        }

        private void UnsubscribeToLevelUpButtons()
        {
            foreach (var button in levelUpButtons)
            {
                button.ActivateButtons();
                button.OnValueChange.RemoveListener(RefreshCost);
            }
        }

        public void CommitPoints()
        {
            if (currentCost > CurrentExperience) return;

            checkpoint.player.CurrencyContainer.RemoveCurrencyFromContainer
                (CurrencyType.Experience,
                currentCost);

            foreach (var button in levelUpButtons)
            {
                checkpoint.player.StatisticHandler.statistics.Find(s => s.type == button.statType).Level = button.currentValue;
            }

            currentExpTMP.text = CurrentExperience.ToString("N0");
            costTMP.text = "<color=yellow>Committed";

            checkpoint.player.StatisticHandler.totalStatLevel += pointsCost;
            checkpoint.player.StatisticHandler.SaveHandlerDetails();
            checkpoint.player.StatisticHandler.ApplyStatistics();
        }

        public void EnterCheckpoint(CheckpointInteractable activeCheckpoint)
        {
            checkpoint = activeCheckpoint;

            checkpoint.player.GetComponent<Pause>().ProhibitPause();

            SetMenu(this);
            ShowCurrentMenu();

            checkpoint.player.PlayerUI.playerDetails.HidePlayerDetails();

            EventSystem.current.SetSelectedGameObject(firstButton);
        }

        public void EnableLevelUpInterface()
        {
            StartCoroutine(levelUpCG.FadeIn(true, true));
            EventSystem.current.SetSelectedGameObject(levelUpFirstButton);

            SubscribeToLevelUpButtons();

            currentExpTMP.text =
                checkpoint.player.CurrencyContainer.GetCurrencyAmount(CurrencyType.Experience).ToString();

            costTMP.text = "";

            pointsCost = 0;
        }

        public void DisableLevelUpInterface()
        {
            StartCoroutine(levelUpCG.FadeOut(true, true));
            EventSystem.current.SetSelectedGameObject(firstButton);

            UnsubscribeToLevelUpButtons();
        }

        public void RefreshCost(int change, LevelUpButton button)
        {
            var sh = checkpoint.player.StatisticHandler;
            var cc = checkpoint.player.CurrencyContainer;

            int newPoints = pointsCost + change;
            int newStatLevel = sh.totalStatLevel + newPoints;

            int newCost = PlayerStatisticHandler.BASE_STATISTIC_COST * newStatLevel;
            int appliedCost = currentCost + newCost;

            if (CurrentExperience - appliedCost < 0 && change > 0)
            {
                button.currentValue--;
                return;
            }

            if (change < 0)
            {
                int prevPoints = pointsCost;
                int prevStatLevel = sh.totalStatLevel + prevPoints;

                int prevCost = PlayerStatisticHandler.BASE_STATISTIC_COST * prevStatLevel;

                appliedCost = currentCost - prevCost;
            }

            pointsCost = newPoints;
            currentCost = appliedCost;

            string currStr = "";

            if (change > 0)
            {
                int advCost = currentCost + PlayerStatisticHandler.BASE_STATISTIC_COST * (newStatLevel + 1);

                Debug.Log(advCost);
                if (advCost > CurrentExperience)
                {
                    currStr = "<color=red>";
                }
            }

            currentExpTMP.text = currStr + (CurrentExperience - currentCost).ToString("N0");

            costTMP.text = pointsCost > 0 ? "<color=red>-" + currentCost.ToString("N0") : "";            
        }

        public void Rest()
        {
            EventSystem.current.enabled = false;
            StartCoroutine(OnRest());
        }

        protected override void OnCancelPerformed(InputAction.CallbackContext callbackContext)
        {
            if (levelUpCG.alpha > 0) DisableLevelUpInterface();
            else OnExitCheckpoint();
        }

        protected override void ShowMenu()
        {
            StartCoroutine(canvasGroup.FadeIn(true, true, 4));
        }

        protected override void HideMenu()
        {
            StartCoroutine(canvasGroup.FadeOut(true, true, 4));
        }

        private IEnumerator OnRest()
        {
            yield return checkpoint.player.PlayerUI.blackFade.FadeIn();
            SceneManager.LoadScene(SceneConstants.LEVEL_1_INDEX);
        }

        public void OnExitCheckpoint()
        {
            checkpoint.ResetCameraBrain();
            checkpoint.player.GetComponentInChildren<HeadLook>().enabled = true;
            checkpoint.player.PlayerAnimator.SetBool("Sitting", false);
            SetMenu(null);
            EventSystem.current.SetSelectedGameObject(null);
            checkpoint.player.PlayerUI.playerDetails.ShowPlayerDetails();
            checkpoint.player.GetComponent<Pause>().AllowPause();
        }
    }
}
