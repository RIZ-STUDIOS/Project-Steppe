using ProjectSteppe.Entities.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.Interactions
{
    public abstract class Interactable : MonoBehaviour
    {
        public abstract string InteractText { get; }
        
        protected PlayerManager player;

        public virtual void Interact() { }

        public virtual void InteractSetup(PlayerManager player)
        {
            this.player = player;
        }
    }
}
