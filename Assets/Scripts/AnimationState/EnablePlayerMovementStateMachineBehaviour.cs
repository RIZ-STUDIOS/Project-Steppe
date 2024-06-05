using ProjectSteppe.Entities.Player;
using UnityEngine;

namespace ProjectSteppe
{
    public class EnablePlayerMovementStateMachineBehaviour : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.GetComponent<PlayerMovementController>().enabled = true;
        }
    }
}
