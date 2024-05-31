using ProjectSteppe.Managers;
using ProjectSteppe.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.Interactions.Interactables
{
    public class PouchInteractable : InventoryInteractable
    {
        public override void Interact()
        {
            if (!Interacted)
            {
                SaveHandler.CurrentSave.pouchesCollected++;
                GameManager.Instance.playerManager.PlayerUsableItemSlot.currentUsable.Charges++;
            }
            base.Interact();
        }
    }
}
