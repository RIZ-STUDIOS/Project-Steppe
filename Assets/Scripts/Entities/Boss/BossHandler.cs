using ProjectSteppe.Entities;
using ProjectSteppe.Entities.Player;
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

        private Animator animator;

        private bool staggered;

        public PlayerManager playerManager;

        private void Awake()
        {
            animator = GetComponent<Animator>();
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
            staggered = true;
            health.ResetBalance();
            health.vulnerable = true;            

            yield return new WaitForSeconds(3);

            health.vulnerable = false;

            animator.SetBool("PostureBreak", false);
            staggered = false;
        }

        public void MortalBlow()
        {
            if (!staggered) return;
            transform.position = playerManager.bossTeleportTransform.position;
            transform.rotation = Quaternion.Euler(0, -playerManager.transform.eulerAngles.y, 0);

            animator.SetTrigger("ForceAnimation");
            animator.SetTrigger("DeathBlow");

            playerManager.PlayerAnimator.SetTrigger("ForceAnimation");
            playerManager.PlayerAnimator.SetTrigger("MortalBlow");
        }
    }
}
