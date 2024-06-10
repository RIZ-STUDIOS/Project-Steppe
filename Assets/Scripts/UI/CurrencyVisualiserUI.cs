using ProjectSteppe.Currencies;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

namespace ProjectSteppe
{
    public class CurrencyVisualiserUI : MonoBehaviour
    {
        [SerializeField]
        private CurrencyContainer container;

        [SerializeField]
        private CurrencyType currencyType;

        [SerializeField]
        private CanvasGroup visCG;

        [SerializeField]
        private CanvasGroup incomingCG;

        [SerializeField]
        private TextMeshProUGUI counterTMP;

        [SerializeField]
        private TextMeshProUGUI incomingTMP;

        [SerializeField]
        private Animator animator;

        [SerializeField]
        private CurrencyIncomingDummy incomingDummy;

        private bool updatingText;
        private int previousAmount;

        [SerializeField]
        private float flavourSpeed = 1;

        private string CurrentCurrency => container.GetCurrencyAmount(currencyType).ToString("N0");

        private void Awake()
        {
            container.OnCurrencyChange.AddListener(UpdateCurrency);
            incomingDummy.OnDispenseWithFlavour.AddListener(UpdateWithFlavour); // Called by animator.
        }

        private void Start()
        {
            counterTMP.text = CurrentCurrency;
            previousAmount = container.GetCurrencyAmount(currencyType);
        }

        private void UpdateCurrency(CurrencyType type, int amount)
        {
            if (type != currencyType || updatingText) return;

            incomingTMP.text = "+ " + amount.ToString("N0");
            animator.Play("IncomingPulse");
            updatingText = true;
        }

        private void UpdateWithFlavour()
        {
            StartCoroutine(UpdateCurrencyWithFlavour());
        }

        private IEnumerator UpdateCurrencyWithFlavour()
        {
            float timer = 0;
            while (timer < 1)
            {
                int amount = Mathf.CeilToInt(Mathf.Lerp(previousAmount, container.GetCurrencyAmount(currencyType), timer));
                counterTMP.text = amount.ToString("N0");

                timer += Time.deltaTime * flavourSpeed * 2;
                yield return null;
                yield return null;
            }

            counterTMP.text = CurrentCurrency;
            previousAmount = container.GetCurrencyAmount(currencyType);

            updatingText = false;
        }
    }
}
