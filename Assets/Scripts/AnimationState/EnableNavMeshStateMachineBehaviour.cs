using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe
{
    public class EnableNavMeshStateMachineBehaviour : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            AIController controller = animator.GetComponent<AIController>();

            controller.navmesh.nextPosition = animator.transform.position;
            controller.navmesh.isStopped = false;
        }
    }
}
