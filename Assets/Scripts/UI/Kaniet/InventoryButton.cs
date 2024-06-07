using ProjectSteppe.Items;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

    public void UpdateData(InventoryItemScriptableObject inventoryItem)
    {
        inventoryItemScriptableObject = inventoryItem;
        buttonText.text = inventoryItemScriptableObject.title.ToUpper();
    }

    private void Awake()
    {
        buttonText = GetComponentInChildren<TMP_Text>();
    }
}
