using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.Entities.Player
{
    public class PlayerManager : MonoBehaviour
    {
        private TargetLock playerTargetLock;

        public TargetLock PlayerTargetLock => this.GetComponentIfNull(ref playerTargetLock);

        private PlayerCapability capabilities;

        public void EnableCapability(PlayerCapability capability)
        {
            this.capabilities |= capability;
        }

        public void DisableCapability(PlayerCapability capability)
        {
            this.capabilities &= ~capability;
        }

        public bool HasCapability(PlayerCapability capability)
        {
            return this.capabilities.HasFlag(capability);
        }
    }

    [System.Flags]
    public enum PlayerCapability
    {
        Move = 1,
        Rotate = 2
    }
}
