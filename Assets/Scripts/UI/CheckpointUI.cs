using Cinemachine;
using ProjectSteppe.Entities.Player;
using ProjectSteppe.Interactions.Interactables;
using ProjectSteppe.UI.Menus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            
        }
    }
}
