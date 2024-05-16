using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        [ContextMenu("Go Back")]
        private void D()
        {
            SetMenu(previousMenu);
        }

        protected override void ShowMenu()
        {
            ShowMenuCoroutine(fadeInSpeed);
            subMenus[0].Show();
        }

        protected override void HideMenu()
        {
            HideMenuCoroutine(fadeOutSpeed);
        }
    }
}
