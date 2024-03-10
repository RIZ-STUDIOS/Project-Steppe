using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class OptionsButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField]
    private List<ResItem> resolutions = new List<ResItem>();
    [SerializeField]
    private TMP_Text resText;
    private int resolutionIndex;

    private PlayerInput playerInput;

    private InputAction rightDpad;
    private InputAction leftDpad;

    private bool isSelected = false;


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
        rightDpad = playerInput.actions["SettingsRight"];
        leftDpad = playerInput.actions["SettingsLeft"];
    }
    private void Update()
    {
        if (isSelected)
        {
            if (rightDpad.triggered)
            {
                ResRight();
            }
            else if (leftDpad.triggered)
            {
                ResLeft();
            }
        }
    }
    
    private void UpdateResolution()
    {
        resText.text = "< " + resolutions[resolutionIndex].horizontal.ToString() + " x " + resolutions[resolutionIndex].vertical.ToString() + " >";
    }
    private void ResLeft()
    {
        resolutionIndex--;
        if(resolutionIndex < 0)
        {
            resolutionIndex = 0;
        }
        UpdateResolution();
    }

    private void ResRight()
    {
        resolutionIndex++;
        if(resolutionIndex > resolutions.Count - 1)
        {
            resolutionIndex = resolutions.Count - 1;
        }
        UpdateResolution();
    }


    [System.Serializable]
    public class ResItem
    {
        public int horizontal, vertical;
    }
}
