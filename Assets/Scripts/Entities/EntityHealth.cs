using RicTools.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace ProjectSteppe.Entities
{
    [RequireComponent(typeof(Entity))]
    public class EntityHealth : EntityBehaviour
    {

        [SerializeField, ReadOnly]
        private float health;
        [SerializeField, ReadOnly]
        private float balance;

        [SerializeField, ReadOnly(AvailableMode.Play), MinValue(1)]
        private float maxHealth;

        [SerializeField, ReadOnly(AvailableMode.Play), MinValue(1)]
        private float maxBalance = 100;

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

        public float Health { get { return health; } private set { health = value; onHealthChange.Invoke(health, maxHealth); } }
        public float Balance { get {  return balance; } private set { balance = value; if (balance < 0) balance = 0; if (balance > maxBalance) balance = maxBalance; onBalanceChange.Invoke(balance, maxBalance); } }

        [Header("Unity Events")]
        public UnityEvent<float, float> onHealthChange;
        [FormerlySerializedAs("onPostureChange")]
        public UnityEvent<float, float> onBalanceChange;
        public UnityEvent onKill;
        public UnityEvent onPostureFull;
        public UnityEvent onHit;

        private float balanceRegenerationTimer;

        private bool invicible;

        public bool vulnerable;

        private Animator animator;

        private void Start()
        {
            Health = maxHealth;
            Balance = balance;
            startBalanceHealthRatio = maxBalance / (float)maxHealth;
            animator = GetComponent<Animator>();
        }

        public void HealHealth(float amount)
        {
            Health += amount;

            if (Health > maxHealth) Health = maxHealth;
        }

        public void DamageHealth(float amount)
        {
            if (vulnerable) amount *= 2;

            Health -= amount;

            if (Health <= 0)
            {
                onKill.Invoke();
                animator.SetBool("PostureBreak", false);
            }
            else
            {
                onHit.Invoke();
            }
        }

        public void ResetHealth()
        {
            Health = maxHealth;
        }

        public void ResetBalance()
        {
            Balance = 0;
        }

        public void DamageBalance(float amount)
        {
            var prevBalance = Balance;
            balanceRegenerationTimer = 0;
            Balance += amount;

            if(prevBalance < maxBalance && Balance >= maxBalance)
            {
                onPostureFull?.Invoke();
            }
        }

        private void Update()
        {
            if(Balance > 0)
            {
                balanceRegenerationTimer += Time.deltaTime;
                if(balanceRegenerationTimer >= timeBeforeBalanceRegeneration)
                {
                    Balance -= maxBalance * balanceRegenerationRate * Time.deltaTime;
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

        [ContextMenu("Kill")]
        private void Kill()
        {
            DamageHealth(maxHealth);
        }

        [ContextMenu("Kill Balance")]
        private void KillBalance()
        {
            DamageBalance(maxBalance);
        }
    }
}
