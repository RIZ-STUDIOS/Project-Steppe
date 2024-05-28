using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.AI.States
{
    [CreateAssetMenu(menuName = "AI/New States/Boss Attack Handler")]
    public class BossAIAttackHandlerState : NewAIAttackHandlerState
    {
        [SerializeField]
        private NewAIAttackState simpleAttackState;

        [SerializeField]
        private NewAIAttackState chargeAttackState;

        [SerializeField]
        private NewAIAttackState swipeAttackState;

        [SerializeField]
        private NewAIAttackState disengageState;

        [SerializeField]
        private NewAIAttackState crazyModeAttackState;

        [SerializeField]
        private NewAIAttackState crazyComboAttackState;

        protected override void ChooseAttack()
        {
            if (currentAttack && currentAttack.attackFinished)
            {
                currentAttack = null;
            }

            if (!currentAttack)
            {
                if (true)
                {
                    currentAttack = GetCopyState(simpleAttackState);
                }else if (true)
                {
                    currentAttack = GetCopyState(chargeAttackState);
                }
                else if (true)
                {
                    currentAttack = GetCopyState(swipeAttackState);
                }
                else if (true)
                {
                    currentAttack = GetCopyState(disengageState);
                }
                else if (true)
                {
                    currentAttack = GetCopyState(crazyModeAttackState);
                }
                else if (true)
                {
                    currentAttack = GetCopyState(crazyComboAttackState);
                }

                if (currentAttack)
                {
                    currentAttack.attackHandler = this;
                    currentAttack.controller = controller;
                    currentAttack.Execute();
                }
            }
        }
    }
}
