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
            if (other.CompareTag("Player"))
            {
                controller.targetTransform = other.transform;

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
            if (other.CompareTag("Player") && other.GetComponent<PlayerManager>())
            {
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
        }

        private IEnumerator PlayerHitCoroutine()
        {
            yield return new WaitForSeconds(timeToResetHitCounter);
            controller.ResetPlayerHits();
        }
    }
}
