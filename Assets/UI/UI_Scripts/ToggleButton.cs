using TMPro;
using UnityEngine;

public class ToggleButton : MonoBehaviour
{
    private SettingsButton settingsButton;

    [SerializeField]
    private TMP_Text text;

    private int index;

    private void Start()
    {
        settingsButton = GetComponent<SettingsButton>();
        settingsButton.onRightAction += ToggleRight;
        settingsButton.onLeftAction += ToggleLeft;
    }

    private void ToggleRight()
    {
        index++;
        if (index > 1)
        {
            index = 0;
        }
        UpdateToggleText();
    }
    private void ToggleLeft()
    {
        index--;
        if (index < 0)
        {
            index = 1;
        }
        UpdateToggleText();
    }

    private void UpdateToggleText()
    {
        if (index == 0)
        {
            text.text = "On";
        }
        else
        {
            text.text = "Off";
        }
    }
}
