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
    private CanvasGroup mainMenu;
    [SerializeField]
    private CanvasGroup generalOptions;
    [SerializeField]
    private CanvasGroup tutorial;

    [SerializeField]
    private GameObject[] optionTabs;

    private bool optionsOn = false;
    private bool tutorialOn = false;
    private bool menuOn = true;

    private int index;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        index = 0;
        cancelAction = playerInput.actions["Cancel"];
        rightBumper = playerInput.actions["RightBumper"];
        leftBumper = playerInput.actions["LeftBumper"];
    }

    private void Update()
    {
        if (cancelAction.triggered)
        {
            BackToMenu();
        }
        if (optionsOn)
        {
            if (rightBumper.triggered)
            {
                optionTabs[index].SetActive(false);
                index = (index + 1) % optionTabs.Length;
                optionTabs[index].SetActive(true);
            }
            else if (leftBumper.triggered)
            {
                optionTabs[index].SetActive(false);
                index = (index - 1 + optionTabs.Length) % optionTabs.Length;
                optionTabs[index].SetActive(true);
            }
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

    public void OpenOptions()
    {
        mainMenu.interactable = false;
        menuOn = false;
        StartCoroutine(Open(generalOptions, true));
        optionsOn = true;
    }

    public void OpenTutorial()
    {
        mainMenu.interactable = false;
        menuOn = false;
        StartCoroutine(Open(tutorial, true));
        tutorialOn = true;
    }

    public void BackToMenu()
    {
        mainMenu.interactable = true;
        if(!menuOn && tutorialOn)
        {
            tutorial.alpha = 0;
            tutorial.interactable = false;
            StartCoroutine(Open(tutorial, false));
            tutorialOn = false;
        }
        else if(!menuOn && optionsOn)
        {
            generalOptions.alpha = 0;
            generalOptions.interactable = false;
            StartCoroutine(Open(generalOptions, false));
            optionsOn = false;
        }
        menuOn = true;
    }
    public IEnumerator Open(CanvasGroup group, bool par)
    {
        if (par)
        {
            for (float f = 0.05f; f <= 1.1f; f += 0.05f)
            {
                group.alpha = f;
                yield return new WaitForSeconds(0.01f);
            }
            group.interactable = true;
        }
        else
        {
            for (float f = 1f; f >= -0.05f; f -= 0.05f)
            {
                group.alpha = f;
                yield return new WaitForSeconds(0.01f);
            }
            group.interactable = false;
        }
    }
}
