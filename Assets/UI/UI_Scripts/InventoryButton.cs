using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using ProjectSteppe.Items;

public class InventoryButton : MonoBehaviour, ISelectHandler
{
    public InventoryItemScriptableObject inventoryItemScriptableObject;

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI typeText;
    public TextMeshProUGUI bodyText;
    public Image icon;

    private TMP_Text buttonText;

    public void OnSelect(BaseEventData baseEventData)
    {
        titleText.text = inventoryItemScriptableObject.title;
        typeText.text = inventoryItemScriptableObject.itemType.ToString().ToUpper();
        bodyText.text = inventoryItemScriptableObject.description;
        icon.sprite = inventoryItemScriptableObject.icon;
    }

    private void Awake()
    {
        buttonText = GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        buttonText.text = inventoryItemScriptableObject.title.ToUpper();
    }
}
