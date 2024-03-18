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

        public TargetLock PlayerTargetLock => this.GetComponentIfNull(ref playerTargetLock);
        public Animator PlayerAnimator => this.GetComponentIfNull(ref playerAnimator);
        public PlayerCamera PlayerCamera => this.GetComponentIfNull(ref playerCamera);
        public PlayerUIManager PlayerUI => this.GetComponentIfNull(ref playerUI);
        public StarterAssetsInputs PlayerInput => this.GetComponentIfNull(ref playerInput);
        public PlayerInteractor PlayerInteractor => this.GetComponentIfNull(ref playerInteractor);

        private PlayerCapability capabilities;

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
            PlayerAnimator.SetTrigger("ForcedExit");
            PlayerAnimator.SetTrigger("Hit");
        }
    }

    [System.Flags]
    public enum PlayerCapability
    {
        Move = 1,
        Rotate = 2,
        Dash = 4
    }
}
