using Cinemachine;
using ProjectSteppe.Entities.Player;
using ProjectSteppe.Interactions.Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe
{
    public class CheckpointUI : MonoBehaviour
    {
        private CheckpointInteractable checkpoint;

        private CanvasGroup canvasGroup;

        private void Awake()
        {
            checkpoint = GetComponent<CheckpointInteractable>();
        }

        public void EnterCheckpoint()
        {
            
        }
    }
}
