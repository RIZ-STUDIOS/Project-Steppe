using UnityEngine;

namespace ProjectSteppe.Audio
{
    public class SoundPlayer : MonoBehaviour
    {
        public AudioSource[] sounds;

        private void Awake()
        {
            sounds = GetComponentsInChildren<AudioSource>();
        }

        public void PlayRandomSound()
        {
            sounds[Random.Range(0, sounds.Length)].Play();
        }
    }
}
