using ProjectSteppe.Entities;
using ProjectSteppe.Entities.Player;
using System.Collections;
using UnityEngine;

namespace ProjectSteppe.AI
{
    public class AICombatController : MonoBehaviour
    {
        private AIController controller;

        private Coroutine hitCoroutine;

        [SerializeField]
        private float timeToResetHitCounter;

        protected virtual void Awake()
        {
            controller = GetComponentInParent<AIController>();
            GetComponentInParent<Entity>().EntityHealth.onHit.AddListener(PlayerHitAssessment);
        }

        private void OnTriggerEnter(Collider other)
        {
            var aiTarget = other.GetComponent<AITarget>();
            if (aiTarget)
            {
                if (aiTarget.currentController == null)
                    aiTarget.currentController = controller;
                controller.targetTransform = aiTarget;

                OnPlayerEnter();

                var playerUI = other.GetComponent<PlayerManager>().PlayerUI;

                playerUI.playerDetails.ShowPlayerDetails();
                playerUI.bossDetails.ShowBossDetails();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            var aiTarget = other.GetComponent<AITarget>();
            if (aiTarget)
            {
                if (aiTarget.currentController == controller)
                    aiTarget.currentController = null;
                controller.targetTransform = null;
                Debug.Log("Left!");
            }
        }

        protected virtual void OnPlayerEnter()
        {

        }

        private void PlayerHitAssessment()
        {
            controller.IncrementPlayerHits();
            Debug.Log("Boss hit " + controller.PlayerHits + " times");
            if(hitCoroutine != null)
            {
                StopCoroutine(hitCoroutine);
                hitCoroutine = null;
            }
            hitCoroutine = StartCoroutine(PlayerHitCoroutine());
        }

        private IEnumerator PlayerHitCoroutine()
        {
            yield return new WaitForSeconds(timeToResetHitCounter);
            controller.ResetPlayerHits();
        }
    }
}
