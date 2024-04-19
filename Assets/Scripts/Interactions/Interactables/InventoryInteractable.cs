using ProjectSteppe.Interactions;
using ProjectSteppe.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.Interactions.Interactables
{
    public class InventoryInteractable : Interactable
    {
        public override string InteractText => "<sprite=8>Pick up";

        public InventoryItemScriptableObject item;

        public int quantity;

        private ParticleSystem interactFX;

        private bool interacted;

        private void Awake()
        {
            interactFX = GetComponentInChildren<ParticleSystem>();
        }

        public override void Interact()
        {
            if (!interacted)
            {
                player.PlayerInventory.items.Add(item);
                interactFX.Stop();
                interacted = true;

                player.PlayerUI.messagePrompt.ShowMessage($"x{quantity} {item.title}");
            }
        }
    }
}
