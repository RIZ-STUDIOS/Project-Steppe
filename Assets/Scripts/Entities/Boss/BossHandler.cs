using ProjectSteppe.Entities;
using ProjectSteppe.Entities.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProjectSteppe
{
    public class BossHandler : MonoBehaviour
    {
        [Header("Phases")]
        [SerializeField]
        private Image[] phaseNodeImages;

        private int currentPhase;

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

        private AIController controller;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            health = GetComponent<EntityHealth>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            controller = GetComponent<AIController>();
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
                navMeshAgent.enabled = true;
            }

            transform.position = playerManager.transform.position + playerManager.transform.forward * 2.36585f;
            transform.rotation = Quaternion.Euler(0, 360 - playerManager.transform.eulerAngles.y, 0);

            health.ResetBalance();
            health.vulnerable = false;

            animator.SetBool("PostureBreak", false);
            staggered = false;

            animator.SetTrigger("ForceAnimation");
            if (currentPhase < phaseNodeImages.Length)
                animator.SetTrigger("DeathBlow");

            playerManager.PlayerAnimator.SetTrigger("ForceAnimation");
            playerManager.PlayerAnimator.SetTrigger("MortalBlow");
        }

        public void HandleEntityDeath()
        {
            if (!staggered)
            {
                health.SetHealth(1);
                health.ForceStagger();
                return;
            }
            phaseNodeImages[currentPhase].color = Color.grey;
            currentPhase++;
            if (currentPhase >= phaseNodeImages.Length)
            {
                OnBossKilled();
                animator.SetTrigger("ForceAnimation");
                animator.SetTrigger("Death");
                controller.enabled = false;
                health.ResetBalance();
                health.SetInvicible(true);
                MortalBlow();
                playerManager.BossDead();
            }
            else
            {
                controller.enabled = false;
                health.ResetBalance();
                health.SetInvicible(true);
                MortalBlow();
                health.ResetHealth();
            }
        }

        // Animation
        private void OnDeathBlowEnd()
        {
            controller.enabled = true;
            health.SetInvicible(false);
        }
    }
}
