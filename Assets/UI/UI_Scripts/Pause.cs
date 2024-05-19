using ProjectSteppe;
using ProjectSteppe.Entities.Player;
using ProjectSteppe.ZedExtensions;
using StarterAssets;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public CanvasGroup pauseMenu;
    public CanvasGroup inventoryMenu;

    public bool paused;

    public GameObject buttonPrefab;
    public GameObject invButtonGOB;
    public GameObject pauseButtonsGOB;

    private PlayerManager player;

    public TextMeshProUGUI title;
    public TextMeshProUGUI itemType;
    public TextMeshProUGUI description;
    public Image itemIcon;

    private bool inventoryOpen;

    private StarterAssetsInputs input;

    private PlayerInput playerInput;

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
        input = GetComponent<StarterAssetsInputs>();
        playerInput = GetComponent<PlayerInput>();
    }

    public void OpenInventory()
    {
        pauseMenu.InstantHide(true, true);

        if (invButtonGOB.transform.childCount != player.PlayerInventory.items.Count)
        {
            for (int i = invButtonGOB.transform.childCount; i < player.PlayerInventory.items.Count; i++)
            {
                var button = Instantiate(buttonPrefab).GetComponent<InventoryButton>();
                button.inventoryItemScriptableObject = player.PlayerInventory.items[i]; ;
                button.titleText = title;
                button.typeText = itemType;
                button.bodyText = description;
                button.icon = itemIcon;
                button.transform.SetParent(invButtonGOB.transform);
            }
        }

        inventoryOpen = true;

        inventoryMenu.InstantShow(true, true);

        EventSystem.current.SetSelectedGameObject(invButtonGOB.transform.GetChild(0).gameObject);
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1;
        LoadingManager.LoadScene(SceneConstants.MAIN_MENU_INDEX);
    }

    private void OnPause()
    {
        paused = !paused;

        if (paused)
        {
            EventSystem.current.SetSelectedGameObject(pauseButtonsGOB.transform.GetChild(0).gameObject);

            playerInput.SwitchCurrentActionMap("UI");
            Time.timeScale = 0;
            input.ResetInputs();
            input.respondToData = false;

            pauseMenu.InstantShow(true, true);
        }
        else
        {
            paused = true;
            OnCancel();
        }
    }

    private void OnCancel()
    {
        if (paused)
        {
            if (inventoryOpen)
            {
                inventoryMenu.InstantHide(true, true);
                inventoryOpen = false;

                pauseMenu.InstantShow(true, true);

                EventSystem.current.SetSelectedGameObject(pauseButtonsGOB.transform.GetChild(0).gameObject);
            }
            else
            {
                playerInput.SwitchCurrentActionMap("Player");
                paused = false;
                Time.timeScale = 1;
                pauseMenu.InstantHide(true, true);
                StartCoroutine(ReEnableControls());
            }
        }
    }

    private IEnumerator ReEnableControls()
    {
        yield return new WaitForEndOfFrame();
        input.respondToData = true;
        //player.EnableCapability(PlayerCapability.Dash);
        //player.EnableCapability(PlayerCapability.Drink);
    }
}
