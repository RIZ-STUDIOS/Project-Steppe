using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;
using ProjectSteppe.Entities.Player;
using UnityEngine.UI;
using ProjectSteppe.ZedExtensions;

public class Pause : MonoBehaviour
{
    public CanvasGroup canvas;
    public CanvasGroup pauseMenu;
    public CanvasGroup invMenu;

    public bool paused;
    private bool isInvOpen;

    public GameObject itemButtonPosition;

    public GameObject itemButton;

    private PlayerManager player;

    public TextMeshProUGUI title;
    public TextMeshProUGUI itemType;
    public TextMeshProUGUI description;
    public Image itemIcon;

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
    }
    private void OnPause()
    {
        paused = !paused;

        if (paused)
        {
            player.DisableCapability(PlayerCapability.Dash);
            player.DisableCapability(PlayerCapability.Drink);

            canvas.InstantShow(true, true);

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
        else if(!paused && !isInvOpen)
        {
            canvas.InstantHide(true, true);
        }
    }

    private void OnCancel()
    {
        if (isInvOpen)
        {
            isInvOpen = false;
            invMenu.alpha = 0;
            invMenu.interactable = false;
            invMenu.blocksRaycasts = false;
            pauseMenu.interactable = true;
            pauseMenu.blocksRaycasts = true;
            EventSystem.current.SetSelectedGameObject(pauseMenu.GetComponentInChildren<Button>().gameObject);
        }
        else
        {
            paused = false;
            canvas.InstantHide(true, true);
        }
    }

    public void Inventory()
    {
        isInvOpen = true;
        invMenu.alpha = 1;
        invMenu.interactable = true;
        invMenu.blocksRaycasts = true;
        pauseMenu.interactable = false;
        pauseMenu.blocksRaycasts = false;
        EventSystem.current.SetSelectedGameObject(invMenu.GetComponentInChildren<Button>().gameObject);
    }

    public void Quit()
    {

    }
}
