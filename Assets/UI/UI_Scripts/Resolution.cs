using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolution : MonoBehaviour
{
    private GeneralOptions generalOptions;
    private SettingsButton settingsButton;
    private void Awake()
    {
        generalOptions = GetComponentInParent<GeneralOptions>();
    }
    private void Start()
    {
        settingsButton = GetComponent<SettingsButton>();
        settingsButton.onRightAction += generalOptions.ResRight;
        settingsButton.onLeftAction += generalOptions.ResLeft;
    }
}