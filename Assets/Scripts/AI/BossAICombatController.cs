using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.AI
{
    public class BossAICombatController : AICombatController
    {
        private bool bossMusicStarted;

        [SerializeField]
        private AudioSource bossMusic;

        protected override void OnPlayerEnter()
        {
            if (!bossMusicStarted)
            {
                bossMusicStarted = true;
                bossMusic.Play();
            }
        }
    }
}
