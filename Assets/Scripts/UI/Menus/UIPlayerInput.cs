using RicTools.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectSteppe.UI
{
    public class UIPlayerInput : SingletonGenericManager<UIPlayerInput>
    {

        public PlayerUIActions playerInput;

        protected override void OnCreation()
        {
            playerInput = new PlayerUIActions();
            playerInput.Enable();
        }
    }
}
