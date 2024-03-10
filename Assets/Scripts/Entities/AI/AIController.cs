using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;

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
        public float distanceFromTarget;

        [Header("States")]
        public AIIdle idle;
        public AIChase chase;
        public AIAttack attack;
        public AIAttackStance stance;

        [Header("Debug")]
        public bool debugEnabled;

        public bool canRotate = true;

        private void Awake()
        {
            combatController = GetComponent<AICombatController>();
            animator = GetComponent<AIAnimator>();
            idle = Instantiate(idle);
            chase = Instantiate(chase);
            stance = Instantiate(stance);
        }

        private void FixedUpdate()
        {
            UpdateState();
            combatController.ManageRecovery(this);
            animator.SetStates(this);
            if (debugEnabled)
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


            if (navmesh.enabled && combatController.currentRecoveryTime <= 0)
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

            if (playerTarget != null)
            {
                distanceFromTarget = Vector3.Distance(transform.position, playerTarget.position);
            }
            else
            {
                isMoving = false;
            }
        }

        private void DebugInfo()
        {
            if (playerTarget != null)
            {
                Debug.DrawLine(eyeLevel.position, playerTarget.transform.position, Color.blue);
            }
        }
    }
}
