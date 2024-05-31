using ProjectSteppe.Entities;
using ProjectSteppe.Entities.Player;
using ProjectSteppe.Managers;
using System.Collections;
using UnityEngine;

namespace ProjectSteppe.AI
{
    public class AICombatController : MonoBehaviour
    {
        protected AIController controller;

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
            }
        }
        private void OnTriggerExit(Collider other)
        {
            var aiTarget = other.GetComponent<AITarget>();
            if (aiTarget)
            {
                OnPlayerExit(aiTarget);

                if (aiTarget.currentController == controller)
                    aiTarget.currentController = null;
                controller.targetTransform = null;

                Debug.Log("Left!");
            }
        }

        protected virtual void OnPlayerEnter()
        {
            GameManager.Instance.playerManager.PlayerUI.playerDetails.ShowPlayerDetails();
            controller.AIEntity.EntityDetails.ShowDetails();
        }

        protected virtual void OnPlayerExit(AITarget aiTarget)
        {
            if(aiTarget.currentController == controller)
            GameManager.Instance.playerManager.PlayerUI.playerDetails.HidePlayerDetails();
            controller.AIEntity.EntityDetails.HideDetails();
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
