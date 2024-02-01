using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.AI;

namespace ProjectSteppe
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private AIState currentState;

        public AICombatController combatController;
        public Transform eyeLevel;
        public Transform playerTarget;
        public NavMeshAgent navmesh;

        [Header("States")]
        public AIIdle idle;
        public AIChase chase;

        private void Awake()
        {
            combatController = GetComponent<AICombatController>();
            idle = Instantiate(idle);
            chase = Instantiate(chase);
        }

        private void FixedUpdate()
        {
            UpdateState();
            DebugInfo();
        }

        private void UpdateState()
        {
            if (currentState == null) return;
            AIState state = currentState.Tick(this);

            if (state != null)
            {
                currentState = state;
            }
        }

        private void DebugInfo()
        {
            if (playerTarget != null)
            {
                Debug.DrawLine(eyeLevel.position, playerTarget.GetComponent<ThirdPersonController>().eyeLevel.transform.position, Color.blue);
            }
        }
    }
}
