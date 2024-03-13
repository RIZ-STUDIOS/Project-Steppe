using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe
{
    public class DisableAIRotationStateMachineBehaviour : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.GetComponent<AIController>().canRotate = false;
        }
    }
}
