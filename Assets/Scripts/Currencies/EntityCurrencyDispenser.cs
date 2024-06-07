using ProjectSteppe.Currencies;
using ProjectSteppe.Entities;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace ProjectSteppe
{
    public class EntityCurrencyDispenser : CurrencyDispenser
    {
        [SerializeField]
        private float dispenseDelay = 3;

        private EntityHealth parentHealth;

        private void Awake()
        {
            parentHealth = GetComponentInParent<EntityHealth>();
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
            parentHealth.mostRecentEntityHitBy.TryGetComponent(out CurrencyContainer container);
            DispenseCurrencyPayloads(container);
            Destroy(gameObject);
        }
    }
}
