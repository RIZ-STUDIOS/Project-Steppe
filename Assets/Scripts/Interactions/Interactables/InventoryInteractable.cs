using ProjectSteppe.Items;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectSteppe.Interactions.Interactables
{
    public class InventoryInteractable : Interactable
    {
        public override string InteractText => "<sprite=8>Pick up";

        public override bool OneTime => true;
        public override bool Interacted { get; protected set; }

        public InventoryItemScriptableObject item;

        public int quantity;

        private ParticleSystem interactFX;

        public UnityEvent OnPickUp;

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

                OnPickUp.Invoke();
            }
        }
    }
}
