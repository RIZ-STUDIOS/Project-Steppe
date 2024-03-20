using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;

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