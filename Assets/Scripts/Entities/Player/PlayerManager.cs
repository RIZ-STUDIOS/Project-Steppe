using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectSteppe.Entities.Player
{
    public class PlayerManager : MonoBehaviour
    {
        private TargetLock playerTargetLock;
        private Animator playerAnimator;

        public TargetLock PlayerTargetLock => this.GetComponentIfNull(ref playerTargetLock);
        public Animator PlayerAnimator => this.GetComponentIfNull(ref playerAnimator);

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
