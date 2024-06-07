using ProjectSteppe.Entities.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe
{
    public class DisablePlayerMovementStateMachineBehaviour : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.GetComponent<PlayerMovementController>().enabled = false;
        }
    }
}
