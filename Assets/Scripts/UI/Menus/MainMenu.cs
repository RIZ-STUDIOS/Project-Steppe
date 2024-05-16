using ProjectSteppe.ZedExtensions;
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

        [SerializeField]
        private CanvasGroup quitCanvasGroup;

        protected override void Awake()
        {
            base.Awake();
            SetMenu(this);
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

        public void QuitGame()
        {
            //if (Application.isEditor) return;
            eventSystem.enabled = false;
            StartCoroutine(QuitGameIEnumerator());
        }

        private IEnumerator QuitGameIEnumerator()
        {
            yield return quitCanvasGroup.FadeIn();
            yield return new WaitForSecondsRealtime(0.1f);
            Application.Quit();
        }

        [ContextMenu("Change to settings menu")]
        public void ShowSettings()
        {
            SetMenu(settingsMenu);
        }
    }
}
