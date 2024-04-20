using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenuArchitecture : MonoBehaviour
{
    public CanvasGroup invGroup;
    public CanvasGroup pauseGroup;

    private Pause pause;

    private bool isInvOpen = false;

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
            EventSystem.current.SetSelectedGameObject(pauseGroup.GetComponentInChildren<Button>().gameObject);
        }
        else if(pause.cancelAction.triggered && !isInvOpen)
        {
            pause.paused = false;
        }
    }
    public void Inventory()
    {
        invGroup.alpha = 1;
        isInvOpen = true;
        EventSystem.current.SetSelectedGameObject(invGroup.GetComponentInChildren<Button>().gameObject);
    }
    public void Quit()
    {
        Debug.Log("Quit");
    }
}
