using ProjectSteppe.UI.Menus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace ProjectSteppe.UI
{
    public class SettingsSwitch : MonoBehaviour
    {
        private Button button;

        public System.Action onRightAction;
        public System.Action onLeftAction;

        private bool isSelected;

        private void Awake()
        {
            button = GetComponent<Button>();

            var et = gameObject.AddComponent<EventTrigger>();

            {
                var e = new EventTrigger.Entry();
                e.eventID = EventTriggerType.Select;
                e.callback = new EventTrigger.TriggerEvent();
                var call = new UnityAction<BaseEventData>(OnSelect);
                e.callback.AddListener(call);
                et.triggers.Add(e);
            }

            {
                var e = new EventTrigger.Entry();
                e.eventID = EventTriggerType.Deselect;
                e.callback = new EventTrigger.TriggerEvent();
                var call = new UnityAction<BaseEventData>(OnUnselect);
                e.callback.AddListener(call);
                et.triggers.Add(e);
            }
        }

        private void Start()
        {
            UIPlayerInput.Instance.playerInput.UI.SettingsRight.performed += OnRightSettingsPerformed;
            UIPlayerInput.Instance.playerInput.UI.SettingsLeft.performed += OnLeftSettingsPerformed;
        }

        private void OnSelect(BaseEventData e)
        {
            isSelected = true;
        }

        private void OnUnselect(BaseEventData e)
        {
            isSelected = false;
        }

        private void OnRightSettingsPerformed(InputAction.CallbackContext context)
        {
            if (!isSelected || !button.enabled || !button.interactable) return;
            onRightAction?.Invoke();
        }

        private void OnLeftSettingsPerformed(InputAction.CallbackContext context)
        {
            if (!isSelected || !button.enabled || !button.interactable) return;
            onLeftAction?.Invoke();
        }

        private void OnDestroy()
        {
            UIPlayerInput.Instance.playerInput.UI.SettingsRight.performed -= OnRightSettingsPerformed;
            UIPlayerInput.Instance.playerInput.UI.SettingsLeft.performed -= OnLeftSettingsPerformed;
        }
    }
}
