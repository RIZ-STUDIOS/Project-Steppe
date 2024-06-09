using ProjectSteppe.Currencies;
using ProjectSteppe.Entities;
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
            if (!parentHealth || !parentHealth.mostRecentEntityHitBy) yield break;
            parentHealth.mostRecentEntityHitBy.TryGetComponent(out CurrencyContainer container);
            DispenseCurrencyPayloads(container);
            Destroy(gameObject);
        }

        public void OnReload()
        {
            Destroy(gameObject);
        }
    }
}
