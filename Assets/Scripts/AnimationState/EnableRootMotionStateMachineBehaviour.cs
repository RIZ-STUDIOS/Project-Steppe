using UnityEngine;

namespace ProjectSteppe
{
    public class EnableRootMotionStateMachineBehaviour : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.applyRootMotion = true;
        }
    }
}
