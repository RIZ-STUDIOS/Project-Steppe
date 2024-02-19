using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private PlayerInput playerInput;

    private InputAction cancelAction;
    private InputAction rightBumper;
    private InputAction leftBumper;

    [SerializeField]
    private CanvasGroup visualsGroup;
    [SerializeField]
    private CanvasGroup audioGroup;
    [SerializeField]
    private CanvasGroup controlsGroup;
    [SerializeField]
    private CanvasGroup mainMenuGroup;



    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        cancelAction = playerInput.actions["Cancel"];
        rightBumper = playerInput.actions["RightBumper"];
        leftBumper = playerInput.actions["LeftBumper"];
    }

    private void Update()
    {
        if (cancelAction.triggered)
        {
            BackToMainMenu();
        }
    }
    public void StartGame()
    {
        //SceneManager.LoadScene(1);
        Debug.Log("Start Game");
    }
    public void Quit()
    {
        //Application.Quit();
        Debug.Log("Quit");
    }

    public void Options()
    {
        ManageGroups(mainMenuGroup, false);
        ManageGroups(visualsGroup, true);
    }

    private void BackToMainMenu()
    {
        ManageGroups(visualsGroup, false);
        ManageGroups(audioGroup, false);
        ManageGroups(controlsGroup, false);
        ManageGroups(mainMenuGroup, true);
    }

    private void ManageGroups(CanvasGroup temp, bool tep)
    {
        if (tep)
        {
            temp.alpha = 1;
            temp.interactable = true;
        }
        else
        {
            temp.alpha = 0;
            temp.interactable = false;
        }
    }
}
