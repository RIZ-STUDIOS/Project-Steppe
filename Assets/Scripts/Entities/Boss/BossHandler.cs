using ProjectSteppe.Entities;
using ProjectSteppe.Entities.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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

        private Animator animator;

        private bool staggered;

        public PlayerManager playerManager;

        private NavMeshAgent navMeshAgent;

        private Coroutine staggerCoroutine;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            health = GetComponent<EntityHealth>();
            health.onKill.AddListener(OnBossKilled);
            navMeshAgent = GetComponent<NavMeshAgent>();
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
            if(staggerCoroutine != null)
            {
                StopCoroutine(staggerCoroutine);
                staggerCoroutine = null;
            }
            staggerCoroutine = StartCoroutine(StaggerTimeoutIEnumerator());
        }

        public IEnumerator StaggerTimeoutIEnumerator()
        {
            staggered = true;
            health.vulnerable = true;
            navMeshAgent.enabled = false;

            yield return new WaitForSeconds(3);

            health.vulnerable = false;

            navMeshAgent.enabled = true;
            animator.SetBool("PostureBreak", false);
            staggered = false;
        }

        public void MortalBlow()
        {
            if (!staggered) return;

            if(staggerCoroutine != null)
            {
                StopCoroutine(staggerCoroutine);
                staggerCoroutine = null;
            }

            transform.position = playerManager.transform.position + playerManager.transform.forward * 2.36585f;
            transform.rotation = Quaternion.Euler(0, -playerManager.transform.eulerAngles.y, 0);

            health.ResetBalance();
            health.vulnerable = false;

            animator.SetBool("PostureBreak", false);
            staggered = false;

            animator.SetTrigger("ForceAnimation");
            if (health.Health > 0)
                animator.SetTrigger("DeathBlow");

            playerManager.PlayerAnimator.SetTrigger("ForceAnimation");
            playerManager.PlayerAnimator.SetTrigger("MortalBlow");
        }
    }
}
