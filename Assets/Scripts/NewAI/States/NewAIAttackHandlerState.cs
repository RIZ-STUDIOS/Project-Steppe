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

        private NewAIAttackState currentAttack;

        [System.NonSerialized]
        public List<string> unusuableStates = new List<string>();

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

            if (currentAttack && currentAttack.attackFinished)
            {
                currentAttack = null;
            }

            if (!currentAttack)
            {
                for(int i = 0; i < attackStates.Length; i++)
                {
                    var attack = Instantiate(attackStates[i]);
                    attack.attackHandler = this;
                    attack.controller = controller;
                    if (attack.UseAttack())
                    {
                        currentAttack = attack;
                        controller.SetPathTo(controller.targetTransform);
                        attack.Execute();
                        controller.NavMeshAgent.ResetPath();
                        return;
                    }
                }
            }
        }

        public override void OnExit()
        {
            if (currentAttack)
            {
                currentAttack.OnForceExit();
            }
        }
    }
}
