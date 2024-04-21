using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ProjectSteppe.Items;
using TMPro;
using ProjectSteppe.Entities.Player;
using UnityEngine.UI;
using ProjectSteppe.ZedExtensions;
using UnityEngine.EventSystems;

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

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
    }

    public void OpenInventory()
    {
        pauseMenu.InstantHide(true, true);

        List<Transform> kiddos = new();
        foreach (Transform child in invButtonGOB.transform)
        {
            kiddos.Add(child);
        }

        foreach (var kid in kiddos)
        {
            Destroy(kid.gameObject);
        }

        for (int i = 0; i < player.PlayerInventory.items.Count; i++)
        {
            var button = Instantiate(buttonPrefab).GetComponent<InventoryButton>();
            button.inventoryItemScriptableObject = player.PlayerInventory.items[i]; ;
            button.titleText = title;
            button.typeText = itemType;
            button.bodyText = description;
            button.icon = itemIcon;
            button.transform.SetParent(invButtonGOB.transform);
        }

        inventoryOpen = true;

        inventoryMenu.InstantShow(true, true);
        
        EventSystem.current.SetSelectedGameObject(invButtonGOB.transform.GetChild(0).gameObject);
    }

    private void OnPause()
    {
        paused = !paused;

        if (paused)
        {
            player.DisableCapability(PlayerCapability.Dash);
            player.DisableCapability(PlayerCapability.Drink);

            pauseMenu.InstantShow(true, true);
        }
        else
        {
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
                paused = false;
                pauseMenu.InstantHide(true, true);
                StartCoroutine(ReEnableControls());
            }
        }
    }

    private IEnumerator ReEnableControls()
    {
        yield return new WaitForEndOfFrame();
        player.EnableCapability(PlayerCapability.Dash);
        player.EnableCapability(PlayerCapability.Drink);
    }
}
