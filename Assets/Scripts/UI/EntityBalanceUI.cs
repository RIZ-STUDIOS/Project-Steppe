using Codice.Client.BaseCommands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectSteppe
{
    public class EntityBalanceUI : MonoBehaviour
    {
        // TESTING
        [Range(0, 1)]
        public float balance;
        // TESTING
        
        [SerializeField]
        private Slider sliderL;

        [SerializeField]
        private Slider sliderR;

        private void OnValidate()
        {
            sliderL.value = balance;
            sliderR.value = balance;
        }
    }
}
