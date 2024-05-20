using ProjectSteppe;
using ProjectSteppe.Entities.Player;
using ProjectSteppe.UI.Menus;
using ProjectSteppe.ZedExtensions;
using StarterAssets;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ProjectSteppe.UI.Menus
{
    public class PauseMenu : MenuBase
    {
        [SerializeField]
        private GameObject defaultSelectable;

        [SerializeField]
        private InventoryMenu inventoryMenu;

        protected override void ShowMenu()
        {
            base.ShowMenu();
            EventSystem.current.SetSelectedGameObject(defaultSelectable);
        }

        public void OpenInventory()
        {
            SetMenu(inventoryMenu);
        }

        public void QuitToMainMenu()
        {
            Time.timeScale = 1;
            LoadingManager.LoadScene(SceneConstants.MAIN_MENU_INDEX);
        }
    }
}
