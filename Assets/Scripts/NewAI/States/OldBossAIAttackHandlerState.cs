using ProjectSteppe.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.AI.States
{
    //[CreateAssetMenu(menuName = "AI/New States/Boss Attack Handler")]
    [System.Obsolete]
    public class OldBossAIAttackHandlerState : NewAIAttackHandlerState
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

        [SerializeField]
        private NewAIAttackState pushBackAttackState;

        [SerializeField]
        private NewAIAttackState pushBackDisengageAttackState;

        [SerializeField]
        private NewBlockAIAttack blockState;

        protected override void ChooseAttack()
        {
            if (currentAttack && currentAttack.attackFinished)
            {
                currentAttack = null;
            }

            // Block
            if(!blocking && controller.playerAttacking && !controller.CommittedToAttack /* potentially add some random value to make boss easier */)
            {
                OnExit();

                currentAttack = GetCopyState(blockState);
                ExecuteAttack();
            }

            if (!currentAttack)
            {
                // 1 -> 2 -> 3
                if (true)
                {
                    currentAttack = GetCopyState(simpleAttackState);
                }
                // 1 -> Charge
                else if (true)
                {
                    currentAttack = GetCopyState(chargeAttackState);
                }
                // 2 -> Swipe
                else if (true)
                {
                    currentAttack = GetCopyState(swipeAttackState);
                }
                // Move back
                else if (true)
                {
                    currentAttack = GetCopyState(disengageState);
                }
                // Crazy Mode
                else if (true)
                {
                    currentAttack = GetCopyState(crazyModeAttackState);
                }
                // 1 -> 2 -> Crazy Mode
                else if (true)
                {
                    currentAttack = GetCopyState(crazyComboAttackState);
                }
                // Push player back and attack
                else if (true)
                {
                    currentAttack = GetCopyState(pushBackAttackState);
                }
                // Push player back and attack, and move back
                else if (true)
                {
                    currentAttack = GetCopyState(pushBackDisengageAttackState);
                }

                if (currentAttack)
                {
                    ExecuteAttack();
                }
            }
        }
    }
}
