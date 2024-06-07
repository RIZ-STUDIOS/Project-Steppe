using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    private InputAction startAction;

    private void Start()
    {
        startAction = new InputAction("ui/start", binding: "<Gamepad>/buttonSouth");
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
