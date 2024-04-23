using UnityEngine;

namespace ProjectSteppe.Interactions.Interactables
{
    public class MessageInteractable : Interactable
    {
        [SerializeField] private string messageText;

        public override string InteractText => "<sprite=8>Read Message";

        public override bool OneTime => false;
        public override bool Interacted { get; protected set; }

        public override void Interact()
        {
            player.PlayerUI.messagePrompt.ShowMessage(messageText);
        }
    }
}
