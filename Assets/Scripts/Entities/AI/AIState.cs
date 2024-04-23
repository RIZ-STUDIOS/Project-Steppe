using UnityEngine;

namespace ProjectSteppe
{
    public class AIState : ScriptableObject
    {
        public virtual AIState Tick(AIController controller)
        {
            return this;
        }

        protected virtual AIState SwitchState(AIController controller, AIState newState)
        {
            ResetState(controller);
            return newState;
        }

        protected virtual void ResetState(AIController controller)
        {

        }
    }
}
