using Cinemachine;
using ProjectSteppe.Entities.Player;
using ProjectSteppe.Interactions.Interactables;
using ProjectSteppe.UI.Menus;
using ProjectSteppe.ZedExtensions;
using System.Collections;
using System.Collections.Generic;
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
            StartCoroutine(canvasGroup.FadeIn(true, true));
        }

        protected override void HideMenu()
        {
            StartCoroutine(canvasGroup.FadeIn(false, false));
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
