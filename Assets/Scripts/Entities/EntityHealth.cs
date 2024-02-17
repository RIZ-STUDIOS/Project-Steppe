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
        private int posture;

        [SerializeField, ReadOnly(AvailableMode.Play), MinValue(1)]
        private int maxHealth;

        [SerializeField, ReadOnly(AvailableMode.Play), MinValue(1)]
        private int maxPosture = 100;

        [SerializeField]
        private float timeBeforePostureRegeneration = 1.5f;

        [SerializeField]
        [Range(0f, 1f)]
        private float postureRegenerationRate = 0.2f;

        [SerializeField]
        [Tooltip("The ratio at which health affects amount of posture.")]
        [Range(0f, 1f)]
        private float postureHealthRegenerationRatio = .5f;

        private float startPostureHealthRatio;

        public int Health { get { return health; } private set { health = value; onHealthChange.Invoke(health, maxHealth); } }
        public int Posture { get {  return posture; } private set { posture = value; if (posture < 0) posture = 0; onPostureChange.Invoke(posture, maxPosture); } }

        [Header("Unity Events")]
        public UnityEvent<int, int> onHealthChange;
        public UnityEvent<int, int> onPostureChange;
        public UnityEvent onKill;
        public UnityEvent onPostureFull;

        private float postureRegenerationTimer;

        private void Start()
        {
            Health = maxHealth;
            Posture = posture;
            startPostureHealthRatio = maxPosture / (float)maxHealth;
        }

        public void DamageHealth(int amount)
        {
            Health -= amount;

            if(Health <= 0)
            {
                onKill.Invoke();
            }
        }

        public void ResetHealth()
        {
            Health = maxHealth;
        }

        public void ResetPosture()
        {
            Posture = posture;
        }

        public void DamagePosture(int amount)
        {
            postureRegenerationTimer = 0;
            Posture += amount;

            if(Posture >= maxPosture)
            {
                onPostureFull.Invoke();
            }
        }

        private void Update()
        {
            if(Posture > 0)
            {
                postureRegenerationTimer += Time.deltaTime;
                if(postureRegenerationTimer >= timeBeforePostureRegeneration)
                {
                    Posture -= Mathf.CeilToInt(maxPosture * postureRegenerationRate * Time.deltaTime);
                }
            }
        }
    }
}
