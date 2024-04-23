using ProjectSteppe.Entities;
using UnityEngine;

namespace ProjectSteppe
{
    public class StopVunerableStateMachineBehaviour : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.GetComponent<EntityHealth>().vulnerable = false;
        }
    }
}
