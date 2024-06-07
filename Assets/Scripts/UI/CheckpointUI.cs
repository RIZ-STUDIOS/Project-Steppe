using Cinemachine;
using ProjectSteppe.Entities.Player;
using ProjectSteppe.Interactions.Interactables;
using ProjectSteppe.UI.Menus;
using ProjectSteppe.ZedExtensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ProjectSteppe.UI
{
    public class CheckpointUI : MenuBase
    {
        private CheckpointInteractable checkpoint;

        [SerializeField]
        private GameObject firstButton;

        [SerializeField]
        private CanvasGroup levelUpCG;

        [SerializeField]
        private GameObject levelUpFirstButton;


        public void EnterCheckpoint(CheckpointInteractable activeCheckpoint)
        {
            checkpoint = activeCheckpoint;

            //checkpoint.player.GetComponent<>

            SetMenu(this);

            checkpoint.player.PlayerUI.playerDetails.HidePlayerDetails();

            StartCoroutine(canvasGroup.FadeIn(true, true));

            EventSystem.current.SetSelectedGameObject(firstButton);
        }

        public void EnableLevelUpInterface()
        {
            StartCoroutine(levelUpCG.FadeIn(true, true));
            EventSystem.current.SetSelectedGameObject(levelUpFirstButton);
        }
    }
}
