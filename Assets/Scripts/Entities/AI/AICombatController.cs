using ProjectSteppe.Entities;
using ProjectSteppe.Entities.Player;
using System.Collections;
using UnityEngine;

namespace ProjectSteppe
{
    public class AICombatController : MonoBehaviour
    {
        public Transform playerTarget;
        public float currentRecoveryTime = 0f;

        public int playerHits;

        private Coroutine hitCoroutine;

        private AIController controller;

        private void Awake()
        {
            controller = GetComponentInParent<AIController>();
            GetComponentInParent<Entity>().EntityHealth.onHit.AddListener(PlayerHitAssessment);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerTarget = other.transform;

                var playerUI = other.GetComponent<PlayerManager>().PlayerUI;

                playerUI.playerDetails.ShowPlayerDetails();
                playerUI.bossDetails.ShowBossDetails();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Left!");
                playerTarget = null;
            }
        }

        public void ManageRecovery(AIController controller)
        {
            if (currentRecoveryTime > 0)
            {
                if (!controller.isMoving)
                {
                    currentRecoveryTime -= Time.fixedDeltaTime;
                }
            }
        }

        public void AddRecovery(float time)
        {
            currentRecoveryTime += time;
        }

        private void PlayerHitAssessment()
        {
            playerHits++;
            Debug.Log("Boss hit " + playerHits + " times");
            if (hitCoroutine != null) StopCoroutine(hitCoroutine);

            hitCoroutine = StartCoroutine(PlayerHitTimeout());
        }

        public IEnumerator PlayerHitTimeout()
        {
            yield return new WaitForSeconds(3);

            hitCoroutine = null;
        }
    }
}
