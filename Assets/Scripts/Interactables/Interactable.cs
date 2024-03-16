using ProjectSteppe.Entities.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.Interactables
{
    public abstract class Interactable : MonoBehaviour
    {
        [NonSerialized] public string interactText;
        
        protected PlayerManager player;

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                player = other.GetComponent<PlayerManager>();
                player.PlayerInteractor.CurrentInteractable = this;
            }
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (player.PlayerInteractor.CurrentInteractable == this) player.PlayerInteractor.CurrentInteractable = null;
            }
        }

        public virtual void Interact() { }
    }
}
