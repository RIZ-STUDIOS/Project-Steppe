using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    private PlayerInput playerInput;

    private InputAction pauseAction;
    [HideInInspector]
    public InputAction cancelAction;

    public bool paused = false;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }
    private void Start()
    {
        pauseAction = playerInput.actions["Pause"];
        cancelAction = playerInput.actions["Cancel"];
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
            Time.timeScale = 0;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
