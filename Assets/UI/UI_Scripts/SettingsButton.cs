using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class SettingsButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private GeneralOptions generalOptions;

    private PlayerInput playerInput;

    private InputAction rightDpad;
    private InputAction leftDpad;

    private bool isSelected = false;

    public delegate void ButtonAction();
    public ButtonAction onRightAction;
    public ButtonAction onLeftAction;

    private GameObject lastSelected;
    public void OnSelect(BaseEventData baseEventData)
    {
        isSelected = true;
    }

    public void OnDeselect(BaseEventData baseEventData)
    {
        isSelected = false;
    }
    private void Awake()
    {
        playerInput = GetComponentInParent<PlayerInput>();
    }
    private void Start()
    {
        generalOptions = GetComponentInParent<GeneralOptions>();
        rightDpad = playerInput.actions["SettingsRight"];
        leftDpad = playerInput.actions["SettingsLeft"];
    }
    private void Update()
    {
        GameObject currentSelected = EventSystem.current.currentSelectedGameObject;
        if (currentSelected != lastSelected)
        {
            if (lastSelected != null)
            {
                ExecuteEvents.Execute(lastSelected, new BaseEventData(EventSystem.current), ExecuteEvents.deselectHandler);
            }
            lastSelected = currentSelected;
        }
        if (isSelected)
        {
            if (rightDpad.triggered)
            {
                onRightAction?.Invoke();
            }
            else if (leftDpad.triggered)
            {
                onLeftAction?.Invoke();
            }
        }
    }
}