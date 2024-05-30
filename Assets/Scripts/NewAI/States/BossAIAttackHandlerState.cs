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
        private NewAIAttackState thurstAttackState;

        [SerializeField]
        private NewAIAttackState aroundTheWorldAttackState;

        [SerializeField]
        private NewAIAttackState heavyAttackState;

        [SerializeField]
        private NewAIAttackState kickAttackState;

        [SerializeField]
        private NewAIAttackState chargeAttackState;

        [SerializeField]
        private NewAIAttackState simpleHeavyAttackState;

        [SerializeField]
        private NewAIAttackState thurstHeavyAttackState;

        [SerializeField]
        private NewAIAttackState thurstAroundTheWorldAttackState;

        [SerializeField]
        private NewAIAttackState kickHeavyAttackState;

        [SerializeField]
        private NewAIAttackState kickChargeAttackState;

        [SerializeField]
        private NewBlockAIAttack blockState;

        protected override void ChooseAttack()
        {
            if (currentAttack && currentAttack.attackFinished)
            {
                currentAttack = null;
            }

            // Block
            if (!blocking && controller.playerAttacking && !controller.CommittedToAttack /* potentially add some random value to make boss easier */)
            {
                OnExit();

                currentAttack = GetCopyState(blockState);
                ExecuteAttack();
            }

            if (!currentAttack)
            {
                if (true)
                {
                    currentAttack = GetCopyState(simpleAttackState);
                }else if (true)
                {
                    currentAttack = GetCopyState(thurstAttackState);
                }

                if (currentAttack)
                {
                    ExecuteAttack();
                }
            }
        }
    }
}
