using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.Interactions.Interactables
{
    public class MessageInteractable : Interactable
    {
        [SerializeField] private string messageText;

        public override string InteractText => "<sprite=8>Read Message";

        public override void Interact()
        {
            player.PlayerUI.messagePrompt.ShowMessage(messageText);
        }
    }
}
