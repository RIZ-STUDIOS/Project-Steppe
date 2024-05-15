using TMPro;
using UnityEngine;

public class InputDevice : MonoBehaviour
{
    private SettingsButton settingsButton;

    [SerializeField]
    private TMP_Text inputDeviceText;

    private int index;

    private void Start()
    {
        settingsButton = GetComponent<SettingsButton>();
        settingsButton.onRightAction += InputDeviceRight;
        settingsButton.onLeftAction += InputDeviceLeft;
    }

    private void InputDeviceRight()
    {
        index++;
        if (index > 1)
        {
            index = 0;
        }
        UpdateInputDeviceText();
    }
    private void InputDeviceLeft()
    {
        index--;
        if (index < 0)
        {
            index = 1;
        }
        UpdateInputDeviceText();
    }

    private void UpdateInputDeviceText()
    {
        if (index == 0)
        {
            inputDeviceText.text = "PS Controller";
        }
        else
        {
            inputDeviceText.text = "Xbox Controller";
        }
    }
}
