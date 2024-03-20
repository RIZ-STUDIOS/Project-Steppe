using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe
{
    public class AICombatAssessment : AIState
    {
        private float playerDistance;
        private int playerHits;

        public override AIState Tick(AIController controller)
        {
            // Assess self
            


            
            // Assess distance
                // > If the target is in range, continue



            // Assess hits against me
                // > If 

            // 





            return SwitchState(controller, controller.chase);
        }

        private IEnumerator PlayerHitsTimeout()
        {
            yield return null;
        }
    }
}
