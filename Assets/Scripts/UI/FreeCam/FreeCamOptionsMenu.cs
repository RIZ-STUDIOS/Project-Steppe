using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace ProjectSteppe.UI
{
    public class FreeCamOptionsMenu : MonoBehaviour
    {
        private Selectable[] selectables;
        private FreeCamera freeCamera;
        private FreeCamOption option;

        private void Awake()
        {
            selectables = GetComponentsInChildren<Selectable>().Where(s=>s.GetComponent<FreeCamOption>()).ToArray();
            for (int i = 0; i < selectables.Length; i++)
            {
                var button = selectables[i];
                var nav = button.navigation;
                if(i != 0)
                {
                    var prevButton = selectables[i - 1];
                    nav.selectOnUp = prevButton;
                }
                if(i != selectables.Length - 1)
                {
                    var nextButton = selectables[i + 1];
                    nav.selectOnDown = nextButton;
                }
                button.navigation = nav;
                var eventTrigger = button.gameObject.AddComponent<EventTrigger>();
                {
                    var e = new EventTrigger.Entry();
                    e.eventID = EventTriggerType.Select;
                    e.callback = new EventTrigger.TriggerEvent();
                    var call = new UnityAction<BaseEventData>(OnSelect);
                    e.callback.AddListener(call);
                    eventTrigger.triggers.Add(e);
                }

                {
                    var e = new EventTrigger.Entry();
                    e.eventID = EventTriggerType.Deselect;
                    e.callback = new EventTrigger.TriggerEvent();
                    var call = new UnityAction<BaseEventData>(OnUnselect);
                    e.callback.AddListener(call);
                    eventTrigger.triggers.Add(e);
                }
            }
        }

        private void Start()
        {
            freeCamera = FreeCamera.instance;
        }

        private void OnSelect(BaseEventData e)
        {
            var opt = e.selectedObject.GetComponent<FreeCamOption>();
            if (!opt) return;
            option = opt;
        }

        private void OnUnselect(BaseEventData e)
        {

        }

        private void OnAction(InputAction.CallbackContext context)
        {
            if (!option) return;
            var value = context.ReadValue<float>();
            if (value < 0)
                option.onLeftAction?.Invoke();
            else if(value >0)
                option.onRightAction?.Invoke();
        }

        public void SelectFirstElement()
        {
            if (selectables.Length == 0) return;
            EventSystem.current.SetSelectedGameObject(selectables[0].gameObject);
            freeCamera.input.Player.Zoom.performed += OnAction;
        }

        public void DisableOptions()
        {
            option = null;
            freeCamera.input.Player.Zoom.performed -= OnAction;
        }
    }
}
