using ProjectSteppe.Entities.Player;
using UnityEngine;

namespace ProjectSteppe
{
    public class RestartAttackStateMachineBehaviour : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var playerAttack = animator.GetComponent<PlayerAttackController>();
            playerAttack.RestartAttack();
        }
    }
}
