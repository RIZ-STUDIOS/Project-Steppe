using System.Collections;
using System.Collections.Generic;
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
        private SettingsSubMenu[] subMenus;

        private int subMenuIndex = 0;

        protected override void ShowMenu()
        {
            ShowMenuCoroutine(fadeInSpeed);
            subMenuIndex = 0;
            subMenus[subMenuIndex].Show();
        }

        protected override void HideMenu()
        {
            subMenus[subMenuIndex].Hide();
            HideMenuCoroutine(fadeOutSpeed);
        }

        private void OnCancel(InputValue value)
        {
            if (value.Get<float>() == 0) return;
            SetMenu(previousMenu);
        }

        private void OnRightBumper(InputValue value)
        {
            if (value.Get<float>() == 0) return;
            if ((subMenuIndex + 1) >= subMenus.Length) return;

            subMenus[subMenuIndex++].Hide();
            subMenus[subMenuIndex].Show();
        }

        private void OnLeftBumper(InputValue value)
        {
            if (value.Get<float>() == 0) return;
            if ((subMenuIndex - 1) < 0) return;

            subMenus[subMenuIndex--].Hide();
            subMenus[subMenuIndex].Show();
        }
    }
}
