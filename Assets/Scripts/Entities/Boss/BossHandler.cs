using ProjectSteppe.AI;
using ProjectSteppe.Entities;
using ProjectSteppe.Entities.Player;
using ProjectSteppe.Managers;
using System.Collections;
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

        [System.NonSerialized]
        public int currentPhase;

        [Header("Targetting")]
        public GameObject lockTarget;

        [Header("Events")]
        public UnityEvent OnBossDeath;

        [Header("Components")]
        public EntityHealth health;

        private Animator animator;

        private bool staggered;

        private PlayerManager playerManager;

        private NavMeshAgent navMeshAgent;

        private Coroutine staggerCoroutine;

        private AIController controller;

        public UnityEvent OnFootstep;
        public UnityEvent OnMortalBlow;

        public float volumeReduceMod = 1;
        public AudioSource bossMusic;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            health = GetComponent<EntityHealth>();
            navMeshAgent = GetComponent<NavMeshAgent>();
                controller = GetComponent<AIController>();
        }

        private void Start()
        {
            playerManager = GameManager.Instance.playerManager;
        }

        public void OnMortalSteppe()
        {
            OnMortalBlow.Invoke();
        }

        public void OnFootsteppe()
        {
            OnFootstep.Invoke();
        }

        private void OnBossKilled()
        {
            StartCoroutine(OnBossKilledIEnumerator());
        }

        private IEnumerator OnBossKilledIEnumerator()
        {
            Destroy(lockTarget);

            StartCoroutine(ReduceBossMusic());

            yield return new WaitForSeconds(4);

            OnBossDeath.Invoke();
        }

        private IEnumerator ReduceBossMusic()
        {
            while (bossMusic.volume > 0)
            {
                bossMusic.volume -= Time.deltaTime * volumeReduceMod;
                yield return null;
            }

            bossMusic.volume = 0;
        }

        public void StaggerTimeout()
        {
            if (staggerCoroutine != null)
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
            if (controller)
                controller.enabled = false;
            navMeshAgent.enabled = false;

            yield return new WaitForSeconds(3);

            health.vulnerable = false;

            navMeshAgent.enabled = true;
            if(controller)
                controller.enabled = true;
            animator.SetBool("PostureBreak", false);
            staggered = false;
        }

        public void MortalBlow()
        {
            if (!staggered) return;

            if (staggerCoroutine != null)
            {
                StopCoroutine(staggerCoroutine);
                staggerCoroutine = null;
                navMeshAgent.enabled = true;
            }

            transform.position = playerManager.transform.position + playerManager.transform.forward * 2.36585f;
            transform.rotation = Quaternion.Euler(0, 360 - playerManager.transform.eulerAngles.y, 0);
            transform.LookAt(playerManager.transform);
            var angle = transform.eulerAngles;
            angle.x = 0;
            angle.z = 0;
            transform.eulerAngles = angle;

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
            health.healthBarIndex = currentPhase;
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
