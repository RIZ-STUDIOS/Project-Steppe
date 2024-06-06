using ProjectSteppe.Currencies;
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

        private int incomingAmount;

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

        private void Awake()
        {
            container.OnCurrencyChange.AddListener(UpdateCurrency);
            GetComponentInChildren<CurrencyIncomingDummy>().OnUpdateText.AddListener(UpdateIncomingText);
        }

        private void Start()
        {
            UpdateIncomingText();
        }

        public void UpdateIncomingText()
        {
            incomingTMP.text = "+ " + incomingAmount;
        }

        private void UpdateCurrency(CurrencyType type, int amount)
        {
            if (type != currencyType) return;

            animator.SetTrigger("Pulse");
            incomingAmount += amount;
            StartCoroutine(UpdateCurrencyWithFlavour());
        }

        private IEnumerator UpdateCurrencyWithFlavour()
        {            
            yield return null;
        }
    }
}
