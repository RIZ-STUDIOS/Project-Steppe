using ProjectSteppe.Currencies;
using ProjectSteppe.Entities;
using ProjectSteppe.Managers;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace ProjectSteppe
{
    public class EntityCurrencyDispenser : CurrencyDispenser, IReloadable
    {
        [SerializeField]
        private float dispenseDelay = 3;

        private void Awake()
        {
            var parentHealth = GetComponentInParent<EntityHealth>();
            if (parentHealth != null) parentHealth.onKill.AddListener(OnParentDeath);
        }

        private void OnParentDeath()
        {
            StartCoroutine(DispenseCurrencyDelayed());
        }

        private IEnumerator DispenseCurrencyDelayed()
        {
            transform.SetParent(null);
            yield return new WaitForSeconds(dispenseDelay);
            DispenseCurrencyPayloads(GameManager.Instance.playerManager.CurrencyContainer);
            Destroy(gameObject);
        }

        public void OnReload()
        {
            Destroy(gameObject);
        }
    }
}
