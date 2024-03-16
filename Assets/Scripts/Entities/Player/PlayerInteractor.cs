using ProjectSteppe.Interactions;
using ProjectSteppe.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.Entities.Player
{
    public class PlayerInteractor : MonoBehaviour
    {
        private Interactable _currentInteractable;
        public Interactable CurrentInteractable
        {
            get { return _currentInteractable; }
            set
            {
                _currentInteractable = value;
                if (_currentInteractable != null) onCurrentInteractableChange?.Invoke(value.interactText);
                else interactPrompt.HidePrompt();
            }
        }

        public System.Action<string> onCurrentInteractableChange;

        private InteractPromptUI interactPrompt;

        private void Awake()
        {
            interactPrompt = GetComponentInChildren<InteractPromptUI>();

            onCurrentInteractableChange += interactPrompt.ShowPrompt;
        }

        private void OnInteract()
        {
            if (CurrentInteractable != null) CurrentInteractable.Interact();
        }
    }
}
