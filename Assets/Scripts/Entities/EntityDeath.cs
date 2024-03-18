using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.Entities
{
    public class EntityDeath : MonoBehaviour
    {
        public ParticleSystem deathFX;
        
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void OnEntityDeath()
        {
            animator.SetTrigger("Death");
            //StartCoroutine(PlayFX());
            //Destroy(gameObject);
        }

        public void OnPostureFull()
        {
            animator.SetTrigger("ForceAnimation");
            animator.SetTrigger("PostureBreak");
        }

        private IEnumerator PlayFX()
        {
            deathFX.transform.SetParent(null);
            deathFX.Play();
            while (deathFX.isPlaying) yield return null;
            Destroy(deathFX.gameObject);
        }
    }
}
