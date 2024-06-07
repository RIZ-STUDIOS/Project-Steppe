using Cinemachine;
using ProjectSteppe.Entities.Player;
using ProjectSteppe.Interactions.Interactables;
using ProjectSteppe.UI.Menus;
using ProjectSteppe.ZedExtensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ProjectSteppe
{
    public class CheckpointUI : MenuBase
    {
        private CheckpointInteractable checkpoint;

        [SerializeField]
        private GameObject firstButton;

        public void EnterCheckpoint(CheckpointInteractable activeCheckpoint)
        {
            checkpoint = activeCheckpoint;

            SetMenu(this);

            checkpoint.player.PlayerUI.playerDetails.HidePlayerDetails();

            StartCoroutine(canvasGroup.FadeIn(fadeSpeedMod: .5f));

            EventSystem.current.SetSelectedGameObject(firstButton);
        }
    }
}
