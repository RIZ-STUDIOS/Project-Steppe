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

        public int Health { get { return health; } private set { health = value; onHealthChange.Invoke(health); } }
        public int Posture { get {  return posture; } private set { posture = value; onPostureChange.Invoke(posture); } }

        [Header("Unity Events")]
        public UnityEvent<int> onHealthChange;
        public UnityEvent<int> onPostureChange;
        public UnityEvent onKill;
        public UnityEvent onPostureFull;

        private void Start()
        {
            Health = maxHealth;
            Posture = posture;
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
            Posture += amount;

            if(Posture >= maxPosture)
            {
                onPostureFull.Invoke();
            }
        }
    }
}
