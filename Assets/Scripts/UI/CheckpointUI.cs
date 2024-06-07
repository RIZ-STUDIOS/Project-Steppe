using Cinemachine;
using ProjectSteppe.Currencies;
using ProjectSteppe.Entities.Player;
using ProjectSteppe.Interactions.Interactables;
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
        private TextMeshProUGUI currencyTMP;

        private int pointsCost;


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

            var buttons = GetComponentsInChildren<LevelUpButton>();
            foreach (var button in buttons)
            {
                button.ActivateButtons();
                button.OnValueChange.AddListener(RefreshCostText);
            }
        }

        public void RefreshCostText(int change, LevelUpButton button)
        {
            var newPoints = pointsCost + change;

            var sh = checkpoint.player.StatisticHandler;
            int costValue = Mathf.CeilToInt(
                sh.BASE_STATISTIC_COST *
                (sh.totalStatLevel + newPoints));

            var cc = checkpoint.player.CurrencyContainer;
            int remainingExp = Mathf.CeilToInt(
                cc.GetCurrencyAmount(CurrencyType.Experience) -
                costValue);

            if (remainingExp < 0)
            {
                button.currentValue--;
                //currencyTMP.text.Append<>
            }

            costTMP.text = pointsCost > 0 ?
                "<color=red>- " + costValue.ToString("N0") :
                "";
            
            currencyTMP.text = pointsCost > 0 ? 
                remainingExp.ToString() :
                checkpoint.player.CurrencyContainer.GetCurrencyAmount(CurrencyType.Experience).ToString(); 
        }

        public void DisableLevelUpInterface()
        {
            StartCoroutine(levelUpCG.FadeOut(true, true));
            EventSystem.current.SetSelectedGameObject(firstButton);
        }

        public void Rest()
        {
            EventSystem.current.enabled = false;
            StartCoroutine(OnRest());
        }

        protected override void OnSubmitPerformed(InputAction.CallbackContext callbackContext)
        {
            if (levelUpCG.alpha > 0)
            {

            }
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
            StartCoroutine(canvasGroup.FadeIn(false, false, 4));
        }

        public IEnumerator OnRest()
        {
            yield return checkpoint.player.PlayerUI.blackFade.FadeIn();
            SceneManager.LoadScene(SceneConstants.LEVEL_1_INDEX);
        }

        private void OnExitCheckpoint()
        {
            checkpoint.ResetCameraBrain();
            checkpoint.player.GetComponentInChildren<HeadLook>().enabled = true;
            checkpoint.player.PlayerAnimator.SetBool("Sitting", false);
            SetMenu(null);
            ShowCurrentMenu();
            EventSystem.current.SetSelectedGameObject(null);
            checkpoint.player.PlayerUI.playerDetails.ShowPlayerDetails();

        }
    }
}
