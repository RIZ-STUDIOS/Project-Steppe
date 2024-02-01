using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe
{
    public class AIState : ScriptableObject
    {
        public virtual AIState Tick(AIController controller)
        {
            return this;
        }
    }
}
