using ProjectSteppe.ScriptableObjects;
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

        public AttackScriptableObject attackScriptableObject;

        public abstract void Execute();

        public abstract bool UseAttack();

        public void FinishAttack()
        {
            attackFinished = true;
        }

        public virtual void OnForceExit()
        {
            controller.animator.SetTrigger("ForceExit");
            //controller.StartCoroutine(CheckTriggerCoroutine());
        }

        private IEnumerator CheckTriggerCoroutine()
        {
            yield return null;
            if (controller.animator.GetBool("ForceExit"))
            {
                controller.animator.ResetTrigger("ForceExit");
            }
        }
    }
}
