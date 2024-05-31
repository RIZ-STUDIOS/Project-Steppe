using UnityEngine;

namespace ProjectSteppe
{
    public class DisableRootMotionStateMachineBehaviour : StateMachineBehaviour
    {
        [SerializeField]
        private bool onEnter;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(onEnter)
            animator.applyRootMotion = false;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(!onEnter)
                animator.applyRootMotion=false;
        }
    }
}
