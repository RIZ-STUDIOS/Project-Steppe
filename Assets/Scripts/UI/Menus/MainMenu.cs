using ProjectSteppe.Saving;
using ProjectSteppe.ZedExtensions;
using System.Collections;
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

        [SerializeField]
        private CanvasGroup progressSpriteCG;
        private Coroutine progressCoroutine;

        protected override void Start()
        {
            base.Start();
            SetMenu(this, true);
        }

        protected override void HideMenu()
        {
            lastSelectedMenuButton = eventSystem.currentSelectedGameObject;
            canvasGroup.blocksRaycasts = false;
            ShowCurrentMenu();
        }

        protected override void ShowMenu()
        {
            if (lastSelectedMenuButton)
                eventSystem.SetSelectedGameObject(lastSelectedMenuButton);
            canvasGroup.blocksRaycasts = true;
        }

        public void QuitGame()
        {
            if (Application.isEditor) return;
            eventSystem.enabled = false;
            playerInput.enabled = false;
            StartCoroutine(QuitGameIEnumerator());
        }

        public void ResetProgress()
        {
            SaveHandler.ResetSave();
            if (progressCoroutine != null) StopCoroutine(progressCoroutine);
            progressCoroutine = StartCoroutine(ProgressResetNotification());
        }

        private IEnumerator ProgressResetNotification()
        {
            progressSpriteCG.alpha = 1;
            while (progressSpriteCG.alpha > 0)
            {
                progressSpriteCG.alpha -= Time.deltaTime;
                yield return null;
            }
        }

        private IEnumerator QuitGameIEnumerator()
        {
            yield return quitCanvasGroup.FadeIn();
            yield return new WaitForSecondsRealtime(0.1f);
            Application.Quit();
        }

        public void ShowSettings()
        {
            SetMenu(settingsMenu);
        }
    }
}
