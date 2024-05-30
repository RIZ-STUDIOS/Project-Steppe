using ProjectSteppe.Entities;
using ProjectSteppe.Entities.Player;
using System.Collections;
using UnityEngine;

namespace ProjectSteppe.AI
{
    public class NewAICombatController : MonoBehaviour
    {
        private NewAIController controller;

        private bool bossMusicStarted;

        public AudioSource bossMusic;

        private Coroutine hitCoroutine;

        [SerializeField]
        private float timeToResetHitCounter;

        private void Awake()
        {
            controller = GetComponentInParent<NewAIController>();
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

                var playerUI = other.GetComponent<PlayerManager>().PlayerUI;

                playerUI.playerDetails.ShowPlayerDetails();
                playerUI.bossDetails.ShowBossDetails();

                if (!bossMusicStarted)
                {
                    bossMusicStarted = true;
                    bossMusic.Play();
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            var aiTarget = other.GetComponent<AITarget>();
            if (aiTarget)
            {
                aiTarget.currentController = null;
                controller.targetTransform = null;
                Debug.Log("Left!");
            }
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
