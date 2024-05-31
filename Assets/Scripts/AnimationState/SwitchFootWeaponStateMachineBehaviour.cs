using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe
{
    public class SwitchFootWeaponStateMachineBehaviour : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var bossHandler = animator.GetComponent<BossHandler>();
            bossHandler.SwitchToFootWeapon();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var bosshandler = animator.GetComponent<BossHandler>();
            bosshandler.SwitchToNormalWeapon();
        }
    }
}
