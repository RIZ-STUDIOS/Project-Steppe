using RicTools.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

namespace ProjectSteppe.UI
{
    public class UIPlayerInput : SingletonGenericManager<UIPlayerInput>
    {
        public PlayerUIActions playerInput;

        public InputDevice currentDevice;

        public System.Action onControlSchemeChanged;

        public ControlScheme controlScheme;

        protected override void OnCreation()
        {
            playerInput = new PlayerUIActions();
            playerInput.Enable();
            
        }

        void Start()
        {
            InputSystem.onEvent += OnDeviceChange;
        }
        void OnDestroy()
        {
            InputSystem.onEvent -= OnDeviceChange;
        }
        private void OnDeviceChange(InputEventPtr eventPtr, InputDevice device)
        {
            if (currentDevice == device) return;

            if (eventPtr.type != StateEvent.Type) return;

            bool validPress = false;
            foreach (InputControl control in eventPtr.EnumerateChangedControls(device, 0.01F))
            {
                validPress = true;
                break;
            }
            if (validPress is false) return;

            if (device is Keyboard || device is Mouse)
            {
                if (controlScheme == ControlScheme.KeyboardMouse) return;
                controlScheme = ControlScheme.KeyboardMouse;
                currentDevice = device;
                onControlSchemeChanged?.Invoke();
            }
            else if (device is Gamepad)
            {
                if (controlScheme == ControlScheme.Gamepad) return;
                controlScheme = ControlScheme.Gamepad;
                currentDevice = device;
                onControlSchemeChanged?.Invoke();
            }

        }
        public enum ControlScheme
        {
            KeyboardMouse = 0, Gamepad = 1 // just need to be same indexes as defined in inputActionAsset
        }
    }
}
