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
            GetComponentInParent<Entity>().EntityHealth.onKill.AddListener(OnEntityDeath);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (controller.GetComponentInParent<EntityHealth>().Health <= 0)
            {
                return;
            }
            var aiTarget = other.GetComponent<AITarget>();
            if (aiTarget)
            {
                if (!aiTarget.nearbyControllers.Contains(controller))
                    aiTarget.nearbyControllers.Add(controller);
                controller.targetEntity = aiTarget.GetComponentInParent<Entity>();

                OnPlayerEnter();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            var aiTarget = other.GetComponent<AITarget>();
            if (aiTarget)
            {

                if (aiTarget.nearbyControllers.Contains(controller))
                    aiTarget.nearbyControllers.Remove(controller);
                OnPlayerExit(aiTarget);
                controller.targetEntity = aiTarget.GetComponentInParent<Entity>();

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
            aiTarget.UpdateControllersList();
            controller.AIEntity.EntityDetails.HideDetails();
        }

        protected virtual void OnEntityDeath()
        {
            RemoveController();
        }

        public void RemoveController()
        {
            GameManager.Instance.playerManager.GetComponent<AITarget>().nearbyControllers.Remove(controller);
            GameManager.Instance.playerManager.GetComponent<AITarget>().UpdateControllersList();
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
