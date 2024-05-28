using ProjectSteppe.Entities;
using ProjectSteppe.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.AI.States
{
    [CreateAssetMenu(menuName = "AI/New States/Attack Handler")]
    public class NewAIAttackHandlerState : NewAIState
    {
        [SerializeField]
        private NewAIState idleState;

        [SerializeField]
        private NewAIState chaseState;

        [SerializeField]
        private NewAIAttackState[] attackStates;

        protected NewAIAttackState currentAttack;

        protected AttackScriptableObject defaultAttackScriptableObject;

        public override void OnEnter()
        {
            defaultAttackScriptableObject = controller.GetComponent<EntityAttacking>().currentAttack;
        }

        public override void Execute()
        {
            if (!controller.targetTransform)
            {
                controller.SwitchAIState(idleState);
                return;
            }

            controller.NavMeshAgent.ResetPath();

            if(Vector3.Distance(controller.transform.position, controller.targetTransform.position) >= controller.distanceToTargetToChase)
            {
                controller.SwitchAIState(chaseState);   
                return;
            }

            ChooseAttack();
        }

        protected virtual void ChooseAttack()
        {
            if (currentAttack && currentAttack.attackFinished)
            {
                currentAttack = null;
            }

            if (!currentAttack)
            {
                for (int i = 0; i < attackStates.Length; i++)
                {
                    var attack = Instantiate(attackStates[i]);
                    attack.attackHandler = this;
                    attack.controller = controller;
                    if (attack.UseAttack())
                    {
                        if (!attack.attackScriptableObject)
                            attack.attackScriptableObject = defaultAttackScriptableObject;
                        currentAttack = attack;
                        controller.SetPathToTarget();
                        controller.GetComponent<EntityAttacking>().currentAttack = attack.attackScriptableObject;
                        attack.Execute();
                        controller.NavMeshAgent.ResetPath();
                        return;
                    }
                }
            }
        }

        public override void OnExit()
        {
            if (currentAttack && !currentAttack.attackFinished)
            {
                currentAttack.OnForceExit();
            }
        }

        protected AttackState GetCopyState<AttackState>(AttackState state) where AttackState : NewAIAttackState
        {
            return Instantiate(state);
        }
    }
}
