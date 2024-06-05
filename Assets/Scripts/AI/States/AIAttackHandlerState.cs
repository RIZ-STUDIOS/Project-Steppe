using ProjectSteppe.Entities;
using ProjectSteppe.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.AI.States
{
    [CreateAssetMenu(menuName = "AI/States/Attack Handler")]
    public class AIAttackHandlerState : AIState
    {
        [System.Serializable]
        private struct AttackData
        {
            public AIAttackState attackState;
            [Range(0,1)]
            public float percentage;
        }

        [SerializeField]
        private AIState idleState;

        [SerializeField]
        private AIState chaseState;

        [SerializeField]
        private AttackData[] attackStates;

        protected AIAttackState currentAttack;

        [System.NonSerialized]
        public AttackScriptableObject defaultAttackScriptableObject;

        [System.NonSerialized]
        public bool blocking;

        private float rotationSpeed;

        public override void OnEnter()
        {
            defaultAttackScriptableObject = controller.GetComponent<EntityAttacking>().currentAttack;
            rotationSpeed = controller.RotationSmoothTime;
        }

        public override void Execute()
        {
            if (!controller.targetEntity)
            {
                controller.SwitchAIState(idleState);
                return;
            }

            if(controller.targetEntity.EntityHealth.Health <= 0)
            {
                if(!currentAttack || (currentAttack && currentAttack.attackFinished))
                    controller.SwitchAIState(idleState);
                return;
            }

            if (controller.NavMeshAgent.hasPath)
                controller.NavMeshAgent.ResetPath();

            if(Vector3.Distance(controller.transform.position, controller.targetEntity.transform.position) >= controller.distanceToTargetToChase && !controller.CommittedToAttack)
            {
                controller.SwitchAIState(chaseState);   
                return;
            }

            //if (controller.targetTransform.currentController != controller) return;

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
                var diceRole = Random.value;
                for (int i = 0; i < attackStates.Length; i++)
                {
                    var attack = Instantiate(attackStates[i].attackState);
                    attack.attackHandler = this;
                    attack.controller = controller;
                    if (attackStates[i].percentage <= diceRole)
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
            controller.RotationSmoothTime = rotationSpeed;
        }

        public override void OnDisable()
        {
            if (currentAttack && !currentAttack.attackFinished)
            {
                controller.UncommitToAttack();
                currentAttack.OnForceExit();
            }
        }

        protected AttackState GetCopyState<AttackState>(AttackState state) where AttackState : AIAttackState
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
            if (currentAttack.changeRotationSpeed)
                controller.RotationSmoothTime = currentAttack.rotationSpeed;
            currentAttack.Execute();
        }

        public bool InAttack()
        {
            return currentAttack && !currentAttack.attackFinished;
        }
    }
}
