using ProjectSteppe.Items;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectSteppe.Interactions.Interactables
{
    public class InventoryInteractable : Interactable
    {
        public string id;

        public override string InteractText => "<sprite=8>Pick up";

        public override bool OneTime => true;

        private bool _interacted;
        public override bool Interacted
        {
            get { return _interacted; }
            set
            {
                _interacted = value;
                if (_interacted)
                {
                    interactFX.Stop();
                }
            }
        }

        public InventoryItemScriptableObject item;

        public int quantity;

        private ParticleSystem interactFX;

        public UnityEvent<InventoryInteractable> OnPickUp;

        private void Awake()
        {
            interactFX = GetComponentInChildren<ParticleSystem>();
        }

        public override void Interact()
        {
            if (!Interacted)
            {
                player.PlayerInventory.items.Add(item);
                interactFX.Stop();
                Interacted = true;

                player.PlayerUI.messagePrompt.ShowMessage($"x{quantity} {item.title}");

                OnPickUp.Invoke(this);
            }
        }
    }
}
