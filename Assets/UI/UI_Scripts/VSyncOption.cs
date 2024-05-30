using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe
{
    public class VSyncOption : MonoBehaviour
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
            settingsButton.onRightAction += generalOptions.VSyncRight;
            settingsButton.onLeftAction += generalOptions.VSyncLeft;
        }
    }
}
