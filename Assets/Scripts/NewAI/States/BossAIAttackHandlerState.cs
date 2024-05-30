using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.AI.States
{
    [CreateAssetMenu(menuName = "AI/New States/Boss Attack Handler")]
    public class BossAIAttackHandlerState : NewAIAttackHandlerState
    {
        [SerializeField]
        private NewAIAttackState fullBasicAttackState;

        [SerializeField]
        private NewAIAttackState thrustAttackState;

        [SerializeField]
        private NewAIAttackState aroundTheWorldAttackState;

        [SerializeField]
        private NewAIAttackState heavyAttackState;

        [SerializeField]
        private NewAIAttackState kickAttackState;

        [SerializeField]
        private NewAIAttackState chargeAttackState;

        [SerializeField]
        private NewAIAttackState fullBasicHeavyAttackState;

        [SerializeField]
        private NewAIAttackState thrustHeavyAttackState;

        [SerializeField]
        private NewAIAttackState thrustAroundTheWorldAttackState;

        [SerializeField]
        private NewAIAttackState kickHeavyAttackState;

        [SerializeField]
        private NewAIAttackState kickChargeAttackState;

        [SerializeField]
        private NewAIAttackState chargeHeavyAttackState;

        [SerializeField]
        private NewAIAttackState buttPokeHeavyAttackState;

        [SerializeField]
        private NewAIAttackState buttPokeAroundTheWorldAttackState;

        [SerializeField]
        private NewBlockAIAttack blockState;

        [SerializeField]
        [Range(0, 1)]
        private float balanceCheckPer;

        [SerializeField]
        [Range(0, 1)]
        private float healthCheckPer;

        private BossHandler bossHandler;

        public override void OnEnter()
        {
            base.OnEnter();
            bossHandler = controller.GetComponent<BossHandler>();
        }

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
                currentAttack = GetCopyState(BalanceCheck());

                if (currentAttack)
                {
                    ExecuteAttack();
                }
            }
        }

        private NewAIAttackState BalanceCheck()
        {
            if(controller.AIEntity.EntityHealth.BalancePer >= balanceCheckPer)
            {
                return HealthCheck();
            }
            return OffensiveCheck();
        }

        private NewAIAttackState HealthCheck()
        {
            if(controller.AIEntity.EntityHealth.HealthPer <= healthCheckPer)
            {
                return DefensiveCheck();
            }
            return OffensiveCheck();
        }

        private NewAIAttackState DefensiveCheck()
        {
            var rollDice = Random.value;
            if (bossHandler.currentPhase == 0)
            {
                if(rollDice <= .6f)
                {
                    return kickAttackState;
                }else if(rollDice < .9f)
                {
                    return thrustAttackState;
                }
                return StandardAttack();
            }
            if(rollDice <= .4f)
            {
                return aroundTheWorldAttackState;
            }else if(rollDice < .6f)
            {
                return kickHeavyAttackState;
            }
            return StandardAdvancedAttack();
        }

        private NewAIAttackState StandardAttack()
        {
            var rollDice = Random.value;
            if(rollDice <= .6f)
            {
                rollDice = Random.value;
                if (rollDice <= .5f)
                    return fullBasicAttackState;
                else if (rollDice < .8f)
                    return thrustAttackState;
                return heavyAttackState;
            }
            rollDice = Random.value;
            if (rollDice <= .25f)
                return fullBasicHeavyAttackState;
            else if (rollDice <= .5f)
                return thrustHeavyAttackState;
            else if (rollDice <= .75f)
                return kickHeavyAttackState;
            return chargeHeavyAttackState;
        }

        private NewAIAttackState StandardAdvancedAttack()
        {
            var rollDice = Random.value;
            if(rollDice <= .4f)
            {
                return AdvancedAttack();
            }
            return StandardAttack();
        }

        private NewAIAttackState AdvancedAttack()
        {
            var rollDice = Random.value;

            if(rollDice <= .2f)
            {
                return aroundTheWorldAttackState;
            }else if(rollDice <= .4f)
            {
                return thrustAroundTheWorldAttackState;
            }else if(rollDice <= .6f)
            {
                return buttPokeAroundTheWorldAttackState;
            }else if(rollDice <= .8f)
            {
                return kickChargeAttackState;
            }
            return buttPokeHeavyAttackState;
        }

        private NewAIAttackState OffensiveCheck()
        {
            var distance = Vector3.Distance(controller.transform.position, controller.targetTransform.transform.position);
            if(distance > controller.distanceToTargetToAttack && distance < controller.distanceToTargetToChase)
            {
                var rollDice = Random.value;
                if (rollDice <= 0.5f)
                    return chargeAttackState;
                return chargeHeavyAttackState;
            }
            if(bossHandler.currentPhase == 0)
            {
                return StandardAttack();
            }
            return StandardAdvancedAttack();
        }
    }
}
