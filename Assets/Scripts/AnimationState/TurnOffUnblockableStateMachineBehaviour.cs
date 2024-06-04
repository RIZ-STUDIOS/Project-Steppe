using ProjectSteppe.AI;
using UnityEngine;

namespace ProjectSteppe
{
    public class TurnOffUnblockableStateMachineBehaviour : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var controller = animator.GetComponent<AIController>();
            controller.TurnOffUnblockable();
        }
    }
}
