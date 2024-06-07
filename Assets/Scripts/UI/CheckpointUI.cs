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


        public void EnterCheckpoint(CheckpointInteractable activeCheckpoint)
        {
            checkpoint = activeCheckpoint;

            checkpoint.player.GetComponent<Pause>().ProhibitPause();

            SetMenu(this);
            ShowCurrentMenu();

            checkpoint.player.PlayerUI.playerDetails.HidePlayerDetails();

            StartCoroutine(canvasGroup.FadeIn(true, true));

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
            //LoadingManager.LoadScene(SceneConstants.LEVEL_1_INDEX);
            EventSystem.current.enabled = false;
            StartCoroutine(OnRest());
        }

        protected override void OnCancelPerformed(InputAction.CallbackContext callbackContext)
        {
            DisableLevelUpInterface();
        }

        public IEnumerator OnRest()
        {
            yield return checkpoint.player.PlayerUI.blackFade.FadeIn();
            SceneManager.LoadScene(SceneConstants.LEVEL_1_INDEX);
        }
    }
}
