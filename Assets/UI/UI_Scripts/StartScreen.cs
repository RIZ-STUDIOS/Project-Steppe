using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class StartScreen : MonoBehaviour
{
    private InputAction startAction;

    private void Start()
    {
        startAction = new InputAction("ui/start", binding: "<Gamepad>/start");
        startAction.Enable();
    }

    private void Update()
    {
        if (startAction.triggered)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
