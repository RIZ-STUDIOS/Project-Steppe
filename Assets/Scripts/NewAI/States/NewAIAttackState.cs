using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.AI.States
{
    public abstract class NewAIAttackState : ScriptableObject
    {
        [System.NonSerialized]
        public NewAIController controller;

        [System.NonSerialized]
        public NewAIAttackHandlerState attackHandler;

        [System.NonSerialized]
        public bool attackFinished;

        public abstract void Execute();

        public abstract bool UseAttack();

        public void FinishAttack()
        {
            attackFinished = true;
        }

        public virtual void OnForceExit()
        {

        }
    }
}
