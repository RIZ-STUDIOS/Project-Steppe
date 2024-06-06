using ProjectSteppe.Currencies;
using RicTools.Attributes;
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

        public float MaxBalance => maxBalance;

        public float MaxHealth => maxHealth;

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

        [System.NonSerialized]
        public int healthBarIndex;

        public float Health { get { return health; } private set { health = value; onHealthChange.Invoke(health, maxHealth); } }
        public float Balance { get { return balance; } private set { balance = value; if (balance < 0) balance = 0; if (balance > maxBalance) balance = maxBalance; onBalanceChange.Invoke(balance, maxBalance); } }

        public float HealthPer => health / maxHealth;

        public float BalancePer => balance / maxBalance;

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

        public Entity mostRecentEntityHitBy;

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

        public void SetHealth(float amount)
        {
            Health = amount;
        }

        public void DamageHealth(float amount)
        {
            if (vulnerable)
            {
                if (GetComponent<BossHandler>()) amount = health;
                else amount *= 2;
            }

            if (Health <= 0) return;

            Health -= amount;

            if (Health <= 0)
            {
                onKill.Invoke();
                animator.SetBool("PostureBreak", false);

                if (TryGetComponent<CurrencyDispenser>(out var currencyDispenser))
                {
                    if (mostRecentEntityHitBy.TryGetComponent<CurrencyContainer>(out var attackerCurrencyContainer))
                    {
                        currencyDispenser.DispenseCurrencyPayloads(attackerCurrencyContainer);
                    }
                }
            }
            else
            {
                onHit.Invoke();
            }
        }

        public void DamageHealth(float amount, Entity attackingEntity)
        {
            mostRecentEntityHitBy = attackingEntity;
            DamageHealth(amount);
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

            if (prevBalance < maxBalance && Balance >= maxBalance)
            {
                onPostureFull?.Invoke();
            }
        }

        public void ForceStagger()
        {
            Balance = MaxBalance;
            balanceRegenerationTimer = 0;
            onPostureFull?.Invoke();
        }

        private void Update()
        {
            if (Balance > 0)
            {
                balanceRegenerationTimer += Time.deltaTime;
                if (balanceRegenerationTimer >= timeBeforeBalanceRegeneration)
                {
                    Balance -= maxBalance * balanceRegenerationRate * Time.deltaTime;
                }
            }
        }

        public void SetInvicible(bool value)
        {
            this.invicible = value;
        }

        [ContextMenu("Invicible")]
        private void BecomeGod()
        {
            SetInvicible(true);
        }

        public void MakeVulnerable()
        {
            vulnerable = true;
        }

        public void StopVunerable()
        {
            vulnerable = false;
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
