using ProjectSteppe.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe
{
    public class DofOption : MonoBehaviour
    {
        private bool on = true;
        private FreeCamOption option;

        private void Awake()
        {
            option = GetComponent<FreeCamOption>();
            option.onLeftAction += () =>
            {
                on = !on;
                UpdateSlowMode();
            };
            option.onRightAction += () =>
            {
                on = !on;
                UpdateSlowMode();
            };
        }

        private void Start()
        {
            UpdateSlowMode();
        }

        private void UpdateSlowMode()
        {
            option.UpdateText(on ? "On" : "Off");
            FreeCamera.instance.SetDoF(on);
        }
    }
}
