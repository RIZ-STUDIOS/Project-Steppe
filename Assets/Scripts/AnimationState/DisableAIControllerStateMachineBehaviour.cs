using ProjectSteppe.AI;
using UnityEngine;

namespace ProjectSteppe
{
    public class DisableAIControllerStateMachineBehaviour : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.GetComponent<AIController>().enabled = false;
        }
    }
}
