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

        [ContextMenu("Go Back")]
        private void D()
        {
            SetMenu(previousMenu);
        }

        protected override void ShowMenu()
        {
            ShowMenuCoroutine(fadeInSpeed);
        }

        protected override void HideMenu()
        {
            HideMenuCoroutine(fadeOutSpeed);
        }
    }
}
