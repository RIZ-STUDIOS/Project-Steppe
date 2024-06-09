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

        [SerializeField]
        private SettingsMenu settingsMenu;

        private GameObject lastSelectedGameObject;

        [System.NonSerialized]
        public Pause pause;

        protected override void ShowMenu()
        {
            base.ShowMenu();
            if (lastSelectedGameObject)
                EventSystem.current.SetSelectedGameObject(lastSelectedGameObject);
            else
                EventSystem.current.SetSelectedGameObject(defaultSelectable);
        }

        protected override void HideMenu()
        {
            if (CurrentMenu == settingsMenu)
                lastSelectedGameObject = EventSystem.current.currentSelectedGameObject;
            else
                lastSelectedGameObject = null;
            EventSystem.current.SetSelectedGameObject(null);
            base.HideMenu();
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

        public void ShowSettings()
        {
            SetMenu(settingsMenu);
        }

        public void GoFreeCam()
        {
            SetMenu(null);
            FreeCamera.instance.enabled = true;
            FreeCamera.instance.menu = this;
        }
    }
}
