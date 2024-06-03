using ProjectSteppe.AI;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectSteppe.Entities
{
    public class EntityDeath : MonoBehaviour
    {
        public ParticleSystem deathFX;

        private Animator animator;

        private EntityHealth health;

        private Coroutine staggerCoroutine;

        private NavMeshAgent navMeshAgent;

        private AIController controller;

        [SerializeField]
        private float staggerLength;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            health = GetComponent<EntityHealth>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            controller = GetComponent<AIController>();
        }

        public void OnEntityDeath()
        {
            animator.SetTrigger("Death");
            animator.SetBool("IsDead", true);
            //StartCoroutine(PlayFX());
            //Destroy(gameObject);
        }

        public void OnPostureFull()
        {
            animator.SetTrigger("ForceAnimation");
            animator.SetBool("PostureBreak", true);
            animator.SetTrigger("PostureBreak");
        }

        private IEnumerator PlayFX()
        {
            deathFX.transform.SetParent(null);
            deathFX.Play();
            while (deathFX.isPlaying) yield return null;
            Destroy(deathFX.gameObject);
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
            health.vulnerable = true;
            if (controller)
                controller.enabled = false;
            navMeshAgent.enabled = false;

            yield return new WaitForSeconds(staggerLength);

            health.vulnerable = false;

            navMeshAgent.enabled = true;
            if (controller)
                controller.enabled = true;
            animator.SetBool("PostureBreak", false);
        }
    }
}
