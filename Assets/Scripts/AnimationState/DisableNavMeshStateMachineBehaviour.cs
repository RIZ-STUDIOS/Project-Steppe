using UnityEngine;

namespace ProjectSteppe
{
    public class DisableNavMeshStateMachineBehaviour : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //animator.GetComponent<AIController>().navmesh.isStopped = true;
        }
    }
}
