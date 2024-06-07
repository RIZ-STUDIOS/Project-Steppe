using ProjectSteppe.Entities.Player;
using UnityEngine;

namespace ProjectSteppe.Interactions
{
    public abstract class Interactable : MonoBehaviour
    {
        public abstract string InteractText { get; }

        public PlayerManager player;

        public abstract bool OneTime { get; }

        public virtual bool CanInteract => true;

        public abstract bool Interacted { get; set; }

        public virtual void Interact() { }

        public virtual void InteractSetup(PlayerManager player)
        {
            this.player = player;
        }
    }
}
