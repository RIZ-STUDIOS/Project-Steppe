using RicTools.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectSteppe.Entities
{
    [RequireComponent(typeof(Entity))]
    public class EntityHealth : EntityBehaviour
    {

        [SerializeField, ReadOnly]
        private int health;
        [SerializeField, ReadOnly]
        private int balance;

        [SerializeField, ReadOnly(AvailableMode.Play), MinValue(1)]
        private int maxHealth;

        [SerializeField, ReadOnly(AvailableMode.Play), MinValue(1)]
        private int maxBalance = 100;

        [SerializeField]
        private float timeBeforeBalanceRegeneration = 1.5f;

        [SerializeField]
        [Range(0f, 1f)]
        private float balanceRegenerationRate = 0.2f;

        [SerializeField]
        [Tooltip("The ratio at which health affects amount of posture.")]
        [Range(0f, 1f)]
        private float balanceHealthRegenerationRatio = .5f;

        private float startBalanceHealthRatio;

        public int Health { get { return health; } private set { health = value; onHealthChange.Invoke(health, maxHealth); } }
        public int Balance { get {  return balance; } private set { balance = value; if (balance < 0) balance = 0; onPostureChange.Invoke(balance, maxBalance); } }

        [Header("Unity Events")]
        public UnityEvent<int, int> onHealthChange;
        public UnityEvent<int, int> onPostureChange;
        public UnityEvent onKill;
        public UnityEvent onPostureFull;
        public UnityEvent onHit;

        private float balanceRegenerationTimer;

        private bool invicible;

        private void Start()
        {
            Health = maxHealth;
            Balance = balance;
            startBalanceHealthRatio = maxBalance / (float)maxHealth;
        }

        public void DamageHealth(int amount)
        {
            Health -= amount;

            onHit.Invoke();

            if(Health <= 0)
            {
                onKill.Invoke();
            }
        }

        public void ResetHealth()
        {
            Health = maxHealth;
        }

        public void ResetBalance()
        {
            Balance = balance;
        }

        public void DamageBalance(int amount)
        {
            balanceRegenerationTimer = 0;
            Balance += amount;

            if(Balance >= maxBalance)
            {
                onPostureFull.Invoke();
            }
        }

        private void Update()
        {
            if(Balance > 0)
            {
                balanceRegenerationTimer += Time.deltaTime;
                if(balanceRegenerationTimer >= timeBeforeBalanceRegeneration)
                {
                    Balance -= Mathf.CeilToInt(maxBalance * balanceRegenerationRate * Time.deltaTime);
                }
            }
        }

        public void SetInvicible(bool value)
        {
            this.invicible = value;
        }

        public bool IsInvicible()
        {
            return invicible;
        }
    }
}
