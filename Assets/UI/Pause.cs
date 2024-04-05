using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    private PlayerInput playerInput;

    private InputAction pauseAction;

    private bool paused = false;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }
    private void Start()
    {
        pauseAction = playerInput.actions["Pause"];
    }
    private void Update()
    {
        CheckPause();
    }

    private void CheckPause()
    {
        if (pauseAction.triggered)
        {
            paused = !paused;
        }

        if (paused)
        {
            pauseMenu.SetActive(true);
        }
        else
        {
            pauseMenu.SetActive(false);
        }
    }
}
