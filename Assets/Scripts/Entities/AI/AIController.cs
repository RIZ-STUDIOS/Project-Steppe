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
        public AIAnimator animator;

        public Transform eyeLevel;
        public Transform playerTarget;
        public NavMeshAgent navmesh;
        public bool isMoving;

        [Header("States")]
        public AIIdle idle;
        public AIChase chase;
        public AIAttack attack;

        private void Awake()
        {
            combatController = GetComponent<AICombatController>();
            animator = GetComponent<AIAnimator>();
            idle = Instantiate(idle);
            chase = Instantiate(chase);
        }

        private void FixedUpdate()
        {
            UpdateState();
            animator.SetStates(this);
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


            if (navmesh.enabled)
            {
                Vector3 destination = navmesh.destination;
                float remainingDistance = Vector3.Distance(destination, transform.position);

                if (remainingDistance > navmesh.stoppingDistance)
                    isMoving = true;
                else
                {
                    isMoving = false;
                }
            }
            else
                isMoving = false;
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
