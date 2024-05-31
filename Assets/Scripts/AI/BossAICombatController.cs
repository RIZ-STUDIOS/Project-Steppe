using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace ProjectSteppe.AI
{
    public class BossAICombatController : AICombatController
    {
        private bool bossMusicStarted;

        [SerializeField]
        private AudioSource bossMusic;

        protected override void OnPlayerEnter()
        {
            base.OnPlayerEnter();
            if (!bossMusicStarted)
            {
                bossMusicStarted = true;
                bossMusic.Play();
            }
        }

        protected override void OnPlayerExit(AITarget aiTarget)
        {

        }
    }
}
