using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuNavigation : MonoBehaviour
{
    private InputAction cancelAction;
    private InputAction rightBumper;
    private InputAction leftBumper;

    public CanvasGroup menuGroup;
    public CanvasGroup optionsGroup;

    public TMP_Text[] optionsSections;

    public int index = 0;
    private void OnDisable()
    {
        cancelAction.Disable();
    }
    private void Start()
    {
        cancelAction = new InputAction("ui/cancel", binding: "<Gamepad>/buttonEast");
        cancelAction.Enable();
        cancelAction.performed += ctx => OnCancel();
        rightBumper = new InputAction("ui/rightbumper", binding: "<Gamepad>/rightShoulder");
        rightBumper.Enable();
        rightBumper.performed += ctx => RightBumper();
        leftBumper = new InputAction("ui/leftbumper", binding: "<Gamepad>/leftShoulder");
        leftBumper.Enable();
        leftBumper.performed += ctx => LeftBumper();
    }

    private void OnCancel()
    {
        Debug.Log("Cancel");
        menuGroup.alpha = 1;
        optionsGroup.alpha = 0;
        menuGroup.interactable = true;
        optionsGroup.interactable = false;
    }
    public void Options()
    {
        Debug.Log("Open Options");
        menuGroup.alpha = 0;
        optionsGroup.alpha = 1;
        menuGroup.interactable = false;
        optionsGroup.interactable = true;
    }

    private void RightBumper()
    {
        Debug.Log("Right Bumper");
        if(index == 2)
        {
            index = 0;
            optionsSections[index].color = Color.green;
            optionsSections[index + 2].color = Color.white;
        }
        else
        {
            index++;
            optionsSections[index].color = Color.green;
            optionsSections[index - 1].color = Color.white;
        }
    }
    private void LeftBumper()
    {
        Debug.Log("Left Bumper");
        if (index == 0)
        {
            index = 2;
            optionsSections[index].color = Color.green;
            optionsSections[index - 2].color = Color.white;
        }
        else
        {
            index--;
            optionsSections[index].color = Color.green;
            optionsSections[index + 1].color = Color.white;
        }
    }
}

