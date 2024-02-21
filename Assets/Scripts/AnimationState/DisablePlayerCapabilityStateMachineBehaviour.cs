using ProjectSteppe.Entities.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe
{
    public class DisablePlayerCapabilityStateMachineBehaviour : StateMachineBehaviour
    {
        [SerializeField]
        private PlayerCapability capability;

        [SerializeField]
        private bool onStateEnter;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (onStateEnter)
            animator.GetComponent<PlayerManager>().DisableCapability(capability);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!onStateEnter)
                animator.GetComponent<PlayerManager>().DisableCapability(capability);
        }
    }
}
