using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.AI
{
    public abstract class AIState : ScriptableObject
    {
        [System.NonSerialized]
        public AIController controller;

        public virtual void OnEnter()
        {

        }

        public abstract void Execute();

        public virtual void OnExit()
        {

        }

        public virtual void OnDisable()
        {

        }
    }
}
