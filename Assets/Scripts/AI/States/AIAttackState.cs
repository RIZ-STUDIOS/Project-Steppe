using ProjectSteppe.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.AI.States
{
    public abstract class AIAttackState : ScriptableObject
    {
        [System.NonSerialized]
        public AIController controller;

        [System.NonSerialized]
        public AIAttackHandlerState attackHandler;

        [System.NonSerialized]
        public bool attackFinished;

        public AttackScriptableObject attackScriptableObject;

        public abstract void Execute();

        public abstract bool CanUseAttack();

        public void FinishAttack()
        {
            attackFinished = true;
        }

        public virtual void OnForceExit()
        {
            controller.animator.SetTrigger("ForceExit");
        }
    }
}
