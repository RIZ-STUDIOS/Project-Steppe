using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FullscreenChange : MonoBehaviour
{
    private SettingsButton settingsButton;

    [SerializeField]
    private TMP_Text text;

    [HideInInspector] public int index;

    private void Start()
    {
        index = 0;
        settingsButton = GetComponent<SettingsButton>();
        settingsButton.onRightAction += FullscreenRight;
        settingsButton.onLeftAction += FullscreenLeft;
    }
    private void Update()
    {
        if (index == 0)
        {
            Screen.fullScreen = true;
        }
        else
        {
            Screen.fullScreen = false;
        }
    }
    private void FullscreenRight()
    {
        index++;
        if (index > 1)
        {
            index = 0;
        }
        UpdateFullscreenText();
    }
    private void FullscreenLeft()
    {
        index--;
        if (index < 0)
        {
            index = 1;
        }
        UpdateFullscreenText();
    }

    private void UpdateFullscreenText()
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
