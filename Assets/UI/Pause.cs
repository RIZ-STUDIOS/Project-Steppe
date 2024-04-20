using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ProjectSteppe.Items;
using TMPro;
using ProjectSteppe.Entities.Player;

public class Pause : MonoBehaviour
{
    public InventoryItemScriptableObject so;
    public GameObject pauseMenu;
    private PlayerInput playerInput;

    private InputAction pauseAction;
    [HideInInspector]
    public InputAction cancelAction;

    public bool paused = false;

    public GameObject itemButtonPosition;

    public GameObject itemButton;
    public RectTransform[] spawnPos;
    private int currentSpawnIndex = 0;

    private PlayerManager player;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        player = GetComponent<PlayerManager>();
    }
    private void Start()
    {
        pauseAction = playerInput.actions["Pause"];
        cancelAction = playerInput.actions["Cancel"];;
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
            //while (itemButtonPosition.transform.childCount > 0) Destroy(itemButtonPosition.transform.GetChild(0).gameObject);
            for (int i = 0; i < player.PlayerInventory.items.Count; i++)
            {
                var item = player.PlayerInventory.items[i];
                var button = Instantiate(itemButton).GetComponent<InventoryButton>();
                button.titleText.text = item.title;
                button.typeText.text = item.itemType.ToString();
                button.bodyText.text = item.description;
                button.icon.sprite = item.icon;
                button.transform.parent = itemButtonPosition.transform;
            }
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
