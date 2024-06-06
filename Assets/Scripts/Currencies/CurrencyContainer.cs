using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectSteppe.Currencies
{
    public class CurrencyContainer : MonoBehaviour
    {
        public UnityEvent<CurrencyType, int> OnCurrencyChange;

        [SerializeField]
        private GameCurrency[] _currencies;

        private void Awake()
        {
            InitContainer();
        }

        public void AddCurrencyToContainer(CurrencyType type, int amount)
        {
            for (int i = 0; i < _currencies.Length; i++)
            {
                var currency = _currencies[i];
                if (currency.type == type)
                {
                    currency.Amount += amount;
                    OnCurrencyChange.Invoke(type, amount);
                    return;
                }
            }
        }

        public void RemoveCurrencyFromContainer(CurrencyType type, int amount)
        {
            AddCurrencyToContainer(type, amount * -1);
        }

        public int GetCurrencyAmount(CurrencyType type)
        {
            for (int i = 0; i < _currencies.Length; i++)
            {
                var currency = _currencies[i];
                if (currency.type == type) return currency.Amount;                
            }

            return -1;
        }

        private void InitContainer()
        {
            _currencies = new GameCurrency[Enum.GetNames(typeof(CurrencyType)).Length];

            for (int i = 0; i < _currencies.Length; i++)
            {
                _currencies[i] = new GameCurrency((CurrencyType)i);
            }
        }
    }
}
