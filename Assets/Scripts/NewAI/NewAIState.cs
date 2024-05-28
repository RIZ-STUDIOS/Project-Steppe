using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.AI
{
    public abstract class NewAIState : ScriptableObject
    {
        [System.NonSerialized]
        public NewAIController controller;

        public virtual void OnEnter()
        {

        }

        public abstract void Execute();

        public virtual void OnExit()
        {

        }
    }
}
