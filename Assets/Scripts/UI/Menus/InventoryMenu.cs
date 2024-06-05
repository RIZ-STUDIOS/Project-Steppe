using ProjectSteppe.Entities.Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

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

        private List<Button> buttons = new List<Button>();

        [SerializeField]
        private UIScrollToSelection itemScroller;

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void ShowMenu()
        {
            SetupInventoryItems();
            base.ShowMenu();
        }

        protected override void OnCancelPerformed(InputAction.CallbackContext callbackContext)
        {
            itemScroller.enabled = false;
            SetMenu(previousMenu);
        }

        private void SetupInventoryItems()
        {
            if (invButtonGOB.transform.childCount != player.PlayerInventory.items.Count)
            {
                for (int i = invButtonGOB.transform.childCount; i < player.PlayerInventory.items.Count; i++)
                {
                    var inventoryButton = Instantiate(buttonPrefab).GetComponent<InventoryButton>();
                    inventoryButton.titleText = title;
                    inventoryButton.typeText = itemType;
                    inventoryButton.bodyText = description;
                    inventoryButton.icon = itemIcon;
                    var button = inventoryButton.GetComponent<Button>();
                    buttons.Add(button);

                    inventoryButton.transform.SetParent(invButtonGOB.transform);
                }
            }

            // Sort List
            player.PlayerInventory.items.Sort((a,b)=>a.title.CompareTo(b.title));

            for (int i = 0; i < player.PlayerInventory.items.Count; i++)
            {
                buttons[i].GetComponent<InventoryButton>().UpdateData(player.PlayerInventory.items[i]);
                var button = buttons[i];
                button.navigation = new Navigation()
                {
                    mode = Navigation.Mode.Explicit,
                    selectOnDown = i == player.PlayerInventory.items.Count - 1 ? buttons[0] : buttons[i + 1],
                    selectOnUp = i == 0 ? buttons[player.PlayerInventory.items.Count - 1] : buttons[i - 1],
                };
            }
            EventSystem.current.SetSelectedGameObject(buttons[0].gameObject);

            StartCoroutine(StartScroller());
        }

        private IEnumerator StartScroller()
        {
            yield return null;
            itemScroller.enabled = true;
        }
    }
}
