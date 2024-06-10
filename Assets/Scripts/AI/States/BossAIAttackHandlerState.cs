using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.AI.States
{
    [CreateAssetMenu(menuName = "AI/New States/Boss Attack Handler")]
    public class BossAIAttackHandlerState : AIAttackHandlerState
    {
        [SerializeField]
        private AIAttackState fullBasicAttackState;

        [SerializeField]
        private AIAttackState thrustAttackState;

        [SerializeField]
        private AIAttackState aroundTheWorldAttackState;

        [SerializeField]
        private AIAttackState heavyAttackState;

        [SerializeField]
        private AIAttackState kickAttackState;

        [SerializeField]
        private AIAttackState chargeAttackState;

        [SerializeField]
        private AIAttackState wideAttackState;

        [SerializeField]
        private AIAttackState lightAttackState;

        [SerializeField]
        private AIAttackState fullBasicHeavyAttackState;

        [SerializeField]
        private AIAttackState thrustHeavyAttackState;

        [SerializeField]
        private AIAttackState thrustAroundTheWorldAttackState;

        [SerializeField]
        private AIAttackState kickHeavyAttackState;

        [SerializeField]
        private AIAttackState kickChargeAttackState;

        [SerializeField]
        private AIAttackState chargeHeavyAttackState;

        [SerializeField]
        private AIAttackState buttPokeHeavyAttackState;

        [SerializeField]
        private AIAttackState buttPokeAroundTheWorldAttackState;

        [SerializeField]
        private AIAttackState lightLightAttackState;

        [SerializeField]
        private AIAttackState wideAroundTheWorldAttackState;

        [SerializeField]
        private BlockAIAttack blockState;

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
            /*if (!blocking && controller.playerAttacking && !controller.CommittedToAttack /* potentially add some random value to make boss easier *///)
            /*{
                OnExit();

                currentAttack = GetCopyState(blockState);
                ExecuteAttack();
            }*/

            if (!currentAttack)
            {
                currentAttack = GetCopyState(BalanceCheck());

                if (currentAttack)
                {
                    ExecuteAttack();
                }
            }
        }

        private AIAttackState BalanceCheck()
        {
            if(controller.AIEntity.EntityHealth.BalancePer >= balanceCheckPer)
            {
                return HealthCheck();
            }
            return OffensiveCheck();
        }

        private AIAttackState HealthCheck()
        {
            if(controller.AIEntity.EntityHealth.HealthPer <= healthCheckPer)
            {
                return DefensiveCheck();
            }
            return OffensiveCheck();
        }

        private AIAttackState DefensiveCheck()
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

        private AIAttackState StandardAttack()
        {
            var rollDice = Random.value;
            if(rollDice <= .6f)
            {
                rollDice = Random.value;
                if (rollDice <= .3f)
                    return fullBasicAttackState;
                else if (rollDice < .5f)
                    return lightAttackState;
                else if (rollDice < .7f)
                    return wideAttackState;
                else if (rollDice < .8f)
                    return thrustAttackState;
                return heavyAttackState;
            }
            rollDice = Random.value;
            if (rollDice <= .20f)
                return fullBasicHeavyAttackState;
            else if (rollDice <= .4f)
                return lightLightAttackState;
            else if (rollDice <= .6f)
                return thrustHeavyAttackState;
            else if (rollDice <= .8f)
                return kickHeavyAttackState;
            return chargeHeavyAttackState;
        }

        private AIAttackState StandardAdvancedAttack()
        {
            var rollDice = Random.value;
            if(rollDice <= .4f)
            {
                return AdvancedAttack();
            }
            return StandardAttack();
        }

        private AIAttackState AdvancedAttack()
        {
            var rollDice = Random.value;

            if(rollDice <= .2f)
            {
                return aroundTheWorldAttackState;
            }
            else if(rollDice <= .3f)
            {
                return wideAroundTheWorldAttackState;
            }
            else if(rollDice <= .4f)
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

        private AIAttackState OffensiveCheck()
        {
            var distance = Vector3.Distance(controller.transform.position, controller.targetEntity.transform.position);
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
