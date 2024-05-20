using RicTools.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectSteppe.UI.Menus
{
    public class UIPlayerInput : GenericManager<UIPlayerInput>
    {
        protected override bool DontDestroyManagerOnLoad => true;

        public PlayerUIActions playerInput;

        protected override void Awake()
        {
            base.Awake();

            playerInput = new PlayerUIActions();
            playerInput.Enable();
        }
    }
}
