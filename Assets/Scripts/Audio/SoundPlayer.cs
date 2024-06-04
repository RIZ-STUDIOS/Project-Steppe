using UnityEngine;

namespace ProjectSteppe.Audio
{
    public class SoundPlayer : MonoBehaviour
    {
        public bool getChildren;
        public AudioSource[] sounds;

        private void Awake()
        {
            if (getChildren) sounds = GetComponentsInChildren<AudioSource>();
        }

        public void PlayRandomSound()
        {
            sounds[Random.Range(0, sounds.Length)].Play();
        }
    }
}
