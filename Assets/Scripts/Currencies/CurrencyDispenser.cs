using ProjectSteppe.Currencies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe
{
    public class CurrencyDispenser : MonoBehaviour
    {
        public GameCurrency[] dispenserPayloads;

        public void DispenseCurrencyPayloads(CurrencyContainer targetContainer)
        {
            for (int i = 0; i < dispenserPayloads.Length; i++)
            {
                var payload = dispenserPayloads[i];
                targetContainer.AddCurrencyToContainer(payload.type, payload.Amount);
            }
        }
    }
}
