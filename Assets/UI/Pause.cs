using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ProjectSteppe.Items;
using TMPro;
using ProjectSteppe.Entities.Player;
using UnityEngine.UI;
using ProjectSteppe.ZedExtensions;

public class Pause : MonoBehaviour
{
    public InventoryItemScriptableObject so;
    public CanvasGroup pauseMenu;
    private PlayerInput playerInput;

    private InputAction pauseAction;
    [HideInInspector]
    public InputAction cancelAction;

    public bool paused;

    public GameObject itemButtonPosition;

    public GameObject itemButton;
    public RectTransform[] spawnPos;
    private int currentSpawnIndex = 0;

    private PlayerManager player;

    public TextMeshProUGUI title;
    public TextMeshProUGUI itemType;
    public TextMeshProUGUI description;
    public Image itemIcon;

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

    private void OnPause()
    {
        paused = !paused;

        if (paused)
        {
            pauseMenu.InstantShow(true, true);

            List<Transform> kiddos = new();
            foreach (Transform child in itemButtonPosition.transform)
            {
                kiddos.Add(child);
            }

            foreach (var kid in kiddos)
            {
                Destroy(kid.gameObject);
            }

            for (int i = 0; i < player.PlayerInventory.items.Count; i++)
            {
                var button = Instantiate(itemButton).GetComponent<InventoryButton>();
                button.inventoryItemScriptableObject = player.PlayerInventory.items[i]; ;
                button.titleText = title;
                button.typeText = itemType;
                button.bodyText = description;
                button.icon = itemIcon;
                button.transform.SetParent(itemButtonPosition.transform);
            }
        }
        else
        {
            pauseMenu.InstantHide(true, true);
        }
    }

    private void OnCancel()
    {
        paused = false;
        pauseMenu.InstantHide(true, true);
    }
}
