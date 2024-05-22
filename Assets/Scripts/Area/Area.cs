using ProjectSteppe.Entities.Player;
using UnityEngine;

namespace ProjectSteppe
{
    public class Area : MonoBehaviour
    {
        [SerializeField] private string areaName;
        [SerializeField] private AudioSource enteredCue;

        private bool entered;

        private void Awake()
        {
            enteredCue = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (!entered)
                {
                    other.GetComponent<PlayerManager>().PlayerUI.areaPrompt.DisplayArea(areaName);
                    entered = true;
                    enteredCue.Play();
                }

            }
        }
    }
}
