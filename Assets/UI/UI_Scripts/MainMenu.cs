using ProjectSteppe.Managers;
using ProjectSteppe.Saving;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private EventSystem eventSystem;

    //private PlayerInput playerInput;

    private InputAction cancelAction;
    private InputAction rightBumper;
    private InputAction leftBumper;

    [SerializeField]
    private CanvasGroup mainMenu;
    [SerializeField]
    private CanvasGroup generalOptions;

    [SerializeField]
    private GameObject[] optionTabs;
    private GameObject lastSelectedMenuButton;

    private bool optionsOn = false;
    private bool menuOn = true;

    private int index;

    [SerializeField]
    private CanvasGroup progressSpriteCG;
    private Coroutine progressCoroutine;

    private void Awake()
    {
        //playerInput = GetComponent<PlayerInput>();
    }
    private void Start()
    {
        index = 0;
        /*cancelAction = playerInput.actions["Cancel"];
        rightBumper = playerInput.actions["RightBumper"];
        leftBumper = playerInput.actions["LeftBumper"];*/
    }

    /*private void Update()
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
                if (index == 2)
                {
                    SetSelectedButton(optionTabs[index].GetComponentInChildren<Slider>().gameObject);
                }
                else
                {
                    SetSelectedButton(optionTabs[index].GetComponentInChildren<Button>().gameObject);
                }
            }
            else if (leftBumper.triggered)
            {
                optionTabs[index].SetActive(false);
                index = (index - 1 + optionTabs.Length) % optionTabs.Length;
                optionTabs[index].SetActive(true);
                if (index == 2)
                {
                    SetSelectedButton(optionTabs[index].GetComponentInChildren<Slider>().gameObject);
                }
                else
                {
                    SetSelectedButton(optionTabs[index].GetComponentInChildren<Button>().gameObject);
                }
            }
        }
    }*/

    public void StartGame()
    {
        GameManager.Instance.RespawnCharacter();
        Debug.Log("Start Game");
    }

    public void ResetProgress()
    {
        SaveHandler.ResetSave();
        if (progressCoroutine != null) StopCoroutine(progressCoroutine);
        progressCoroutine = StartCoroutine(ProgressResetNotification());
    }

    private IEnumerator ProgressResetNotification()
    {
        progressSpriteCG.alpha = 1;
        while (progressSpriteCG.alpha > 0)
        {
            progressSpriteCG.alpha -= Time.deltaTime;
            yield return null;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void OpenOptions()
    {
        ResetOptionsTab();
        lastSelectedMenuButton = eventSystem.currentSelectedGameObject;
        mainMenu.interactable = false;
        menuOn = false;
        StartCoroutine(Open(generalOptions, true));
        optionsOn = true;
        SetSelectedButton(optionTabs[index].GetComponentInChildren<Button>().gameObject);
    }
    public void BackToMenu()
    {
        if (!menuOn && optionsOn)
        {
            StartCoroutine(Open(generalOptions, false));
            optionsOn = false;
            SetSelectedButton(lastSelectedMenuButton);
        }
        mainMenu.interactable = true;
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
    private void SetSelectedButton(GameObject gameObject)
    {
        eventSystem.SetSelectedGameObject(gameObject);
    }
    private void ResetOptionsTab()
    {
        optionTabs[index].SetActive(false);
        index = 0;
        optionTabs[index].SetActive(true);
    }
}
