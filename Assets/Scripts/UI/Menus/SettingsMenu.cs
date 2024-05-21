using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectSteppe.UI.Menus
{
    public class SettingsMenu : MenuBase
    {
        [SerializeField]
        private float fadeInSpeed = 5;

        [SerializeField]
        private float fadeOutSpeed = 5;

        [SerializeField]
        private bool instant;

        [SerializeField]
        private SettingsSubMenu[] subMenus;

        private int subMenuIndex = 0;

        protected override void ShowMenu()
        {
            if (instant)
                base.ShowMenu();
            else
            ShowMenuCoroutine(fadeInSpeed);
            subMenuIndex = 0;
            subMenus[subMenuIndex].Show();
        }

        protected override void HideMenu()
        {
            subMenus[subMenuIndex].Hide();
            if (instant)
                base.HideMenu();
            else
            HideMenuCoroutine(fadeOutSpeed);
        }

        protected override void OnCancelPerformed(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.ReadValue<float>() == 0) return;
            SetMenu(previousMenu);
        }

        protected override void OnRightBumperPerformed(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.ReadValue<float>() == 0) return;
            if ((subMenuIndex + 1) >= subMenus.Length) return;

            subMenus[subMenuIndex++].Hide();
            subMenus[subMenuIndex].Show();
        }

        protected override void OnLeftBumperPerformed(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.ReadValue<float>() == 0) return;
            if ((subMenuIndex - 1) < 0) return;

            subMenus[subMenuIndex--].Hide();
            subMenus[subMenuIndex].Show();
        }
    }
}
