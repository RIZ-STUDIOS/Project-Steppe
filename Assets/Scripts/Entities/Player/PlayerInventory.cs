using ProjectSteppe.Items;
using ProjectSteppe.Managers;
using ProjectSteppe.Saving;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.Entities.Player
{
    public class PlayerInventory : MonoBehaviour
    {
        public List<InventoryItemScriptableObject> items = new List<InventoryItemScriptableObject>();

        private void Start()
        {
            List<InventoryItemScriptableObject> savedItems = new();

            for (int i = 0; i < items.Count; i++)
            {
                if (!SaveHandler.CurrentSave.playerInventoryIDs.Contains(items[i].title))
                {
                    SaveHandler.CurrentSave.playerInventoryIDs.Add(items[i].title);
                }
            }

            for (int i = 0; i < GameManager.Instance.availableItems.items.Length; i++)
            {
                var item = GameManager.Instance.availableItems.items[i];

                if (SaveHandler.CurrentSave.playerInventoryIDs.Contains(item.title))
                {
                    savedItems.Add(item);
                }
            }

            items = savedItems;

            SaveHandler.SaveGame();
        }
    }
}
