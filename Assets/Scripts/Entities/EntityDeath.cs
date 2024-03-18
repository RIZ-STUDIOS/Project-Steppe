using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.Entities
{
    public class EntityDeath : MonoBehaviour
    {
        public ParticleSystem deathFX;
        
        public void OnEntityDeath()
        {
            StartCoroutine(PlayFX());
            //Destroy(gameObject);
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
