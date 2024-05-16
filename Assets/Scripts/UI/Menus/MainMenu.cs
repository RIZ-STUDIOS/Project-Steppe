using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.UI.Menus
{
    public class MainMenu : MenuBase
    {
        [SerializeField]
        private SettingsMenu settingsMenu;

        private GameObject lastSelectedMenuButton;

        protected override void Awake()
        {
            base.Awake();
            SetMenu(this);
        }

        [ContextMenu("Change to settings menu")]
        public void ShowSettings()
        {
            SetMenu(settingsMenu);
        }

        protected override void HideMenu()
        {
            lastSelectedMenuButton = eventSystem.currentSelectedGameObject;
            canvasGroup.blocksRaycasts = false;
            ShowCurrentMenu();
        }

        protected override void ShowMenu()
        {
            if(lastSelectedMenuButton)
            eventSystem.SetSelectedGameObject(lastSelectedMenuButton);
            canvasGroup.blocksRaycasts = true;
        }
    }
}
