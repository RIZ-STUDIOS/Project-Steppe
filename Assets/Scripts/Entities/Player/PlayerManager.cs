using ProjectSteppe.UI;
using StarterAssets;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace ProjectSteppe.Entities.Player
{
    public class PlayerManager : MonoBehaviour
    {
        private TargetLock playerTargetLock;
        private Animator playerAnimator;
        private PlayerCamera playerCamera;
        private PlayerUIManager playerUI;
        private StarterAssetsInputs playerInput;
        private PlayerInteractor playerInteractor;
        private Entity playerEntity;

        public Transform bossTeleportTransform;

        public TargetLock PlayerTargetLock => this.GetComponentIfNull(ref playerTargetLock);
        public Animator PlayerAnimator => this.GetComponentIfNull(ref playerAnimator);
        public PlayerCamera PlayerCamera => this.GetComponentIfNull(ref playerCamera);
        public PlayerUIManager PlayerUI => this.GetComponentIfNull(ref playerUI);
        public StarterAssetsInputs PlayerInput => this.GetComponentIfNull(ref playerInput);
        public PlayerInteractor PlayerInteractor => this.GetComponentIfNull(ref playerInteractor);
        public Entity PlayerEntity => this.GetComponentIfNull(ref playerEntity);

        private PlayerCapability capabilities = (PlayerCapability)0b111111;

        public UnityEvent onCapabilityChange;

        public void EnableCapability(PlayerCapability capability)
        {
            this.capabilities |= capability;
            onCapabilityChange.Invoke();
        }

        public void DisableCapability(PlayerCapability capability)
        {
            this.capabilities &= ~capability;
            onCapabilityChange.Invoke();
        }

        public bool HasCapability(PlayerCapability capability)
        {
            return this.capabilities.HasFlag(capability);
        }

        public void DoHit()
        {
            PlayerAnimator.SetTrigger("ForceAnimation");
            PlayerAnimator.SetTrigger("Hit");
        }
    }

    [System.Flags]
    public enum PlayerCapability
    {
        Move = 1,
        Rotate = 2,
        Dash = 4,
        Drink = 8,
        Sit = 16,
        Attack = 32
    }
}
