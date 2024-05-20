using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectSteppe.UI.Menus
{
    public class InventoryMenu : MenuBase
    {
        protected override void OnCancelPerformed(InputAction.CallbackContext callbackContext)
        {
            SetMenu(previousMenu);
        }

        private void SetupInventoryItems()
        {

        }
    }
}
