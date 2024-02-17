using Codice.Client.BaseCommands;
using ProjectSteppe.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProjectSteppe
{
    public class EntityBalanceUI : MonoBehaviour
    {
        [SerializeField]
        private EntityHealth entityHealth;

        [SerializeField]
        private Slider sliderL;

        [SerializeField]
        private Slider sliderR;

        private void Start()
        {
            entityHealth.onPostureChange.AddListener(OnBalanceChange);
        }

        private void OnBalanceChange(int balance, int maxBalance)
        {
            sliderL.value = balance;
            sliderR.value = balance;
        }
    }
}
