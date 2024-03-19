using ProjectSteppe.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectSteppe
{
    public class BossHandler : MonoBehaviour
    {
        [Header("Targetting")]
        public GameObject lockTarget;

        [Header("Events")]
        public UnityEvent OnBossDeath;

        [Header("Components")]
        public EntityHealth health;

        private void Awake()
        {
            health = GetComponent<EntityHealth>();
            health.onKill.AddListener(OnBossKilled);
        }

        private void OnBossKilled()
        {
            StartCoroutine(OnBossKilledIEnumerator());
        }

        private IEnumerator OnBossKilledIEnumerator()
        {
            Destroy(lockTarget);

            yield return new WaitForSeconds(4);

            OnBossDeath.Invoke();
        }

        public void StaggerTimeout()
        {
            StartCoroutine(StaggerTimeoutIEnumerator());
        }

        public IEnumerator StaggerTimeoutIEnumerator()
        {
            health.ResetBalance();
            health.vulnerable = true;            

            yield return new WaitForSeconds(3);

            health.vulnerable = false;

            GetComponent<Animator>().SetBool("PostureBreak", false);
        }
    }
}
