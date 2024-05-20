using Codice.Client.BaseCommands.Merge.Xml;
using GluonGui.WorkspaceWindow.Views.WorkspaceExplorer;
using ProjectSteppe.Entities.Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace ProjectSteppe.UI.Menus
{
    public class InventoryMenu : MenuBase
    {
        [SerializeField]
        private PlayerManager player;

        [SerializeField]
        private GameObject buttonPrefab;

        [SerializeField]
        private GameObject invButtonGOB;

        [SerializeField]
        private TextMeshProUGUI title;
        [SerializeField]
        private TextMeshProUGUI itemType;
        [SerializeField]
        private TextMeshProUGUI description;
        [SerializeField]
        private Image itemIcon;

        protected override void ShowMenu()
        {
            SetupInventoryItems();
            base.ShowMenu();
        }

        protected override void OnCancelPerformed(InputAction.CallbackContext callbackContext)
        {
            SetMenu(previousMenu);
        }

        private void SetupInventoryItems()
        {
            if (invButtonGOB.transform.childCount != player.PlayerInventory.items.Count)
            {
                for (int i = invButtonGOB.transform.childCount; i < player.PlayerInventory.items.Count; i++)
                {
                    var button = Instantiate(buttonPrefab).GetComponent<InventoryButton>();
                    button.inventoryItemScriptableObject = player.PlayerInventory.items[i];
                    button.titleText = title;
                    button.typeText = itemType;
                    button.bodyText = description;
                    button.icon = itemIcon;
                    button.transform.SetParent(invButtonGOB.transform);
                }
            }
            EventSystem.current.SetSelectedGameObject(invButtonGOB.transform.GetChild(0).gameObject);
        }
    }
}
