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

        [System.NonSerialized]
        public AttackScriptableObject defaultAttackScriptableObject;

        [System.NonSerialized]
        public bool blocking;

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
                    if (attack.CanUseAttack())
                    {
                        currentAttack = attack;
                        ExecuteAttack();
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
            controller.AIEntity.EntityAttacking.currentAttack = defaultAttackScriptableObject;
        }

        protected AttackState GetCopyState<AttackState>(AttackState state) where AttackState : NewAIAttackState
        {
            if(!state) return null;
            return Instantiate(state);
        }

        protected void ExecuteAttack()
        {
            if (!currentAttack.attackScriptableObject)
                currentAttack.attackScriptableObject = defaultAttackScriptableObject;
            currentAttack.attackHandler = this;
            currentAttack.controller = controller;
            if (controller.animator.GetBool("ForceExit"))
            {
                controller.animator.ResetTrigger("ForceExit");
            }
            controller.AIEntity.EntityAttacking.currentAttack = currentAttack.attackScriptableObject;
            currentAttack.Execute();
        }
    }
}
