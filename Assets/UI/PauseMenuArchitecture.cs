using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseMenuArchitecture : MonoBehaviour
{
    public CanvasGroup invGroup;
    public CanvasGroup settingsGroup;
    public CanvasGroup pauseGroup;

    private Pause pause;

    private bool isInvOpen = false;
    private bool isSettingsOpen = false;
    private bool isPauseOpen = true;

    private void Start()
    {
        pause = FindObjectOfType<Pause>();
    }

    private void Update()
    {
        if (pause.cancelAction.triggered && isInvOpen)
        {
            invGroup.alpha = 0;
            isInvOpen = false;
            isPauseOpen = true;
        }
        else if(pause.cancelAction.triggered && isPauseOpen)
        {
            pause.paused = false;
        }
    }
    public void Inventory()
    {
        invGroup.alpha = 1;
        isInvOpen = true;
        isPauseOpen = false;
    }
    public void Quit()
    {
        Debug.Log("Quit");
    }

    public void Settings()
    {
        settingsGroup.alpha = 1;
        settingsGroup.interactable = true;
    }
}
