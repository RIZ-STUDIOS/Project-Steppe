using UnityEngine;
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

        [Header("Movement")]
        public NavMeshAgent navmesh;
        public bool isMoving;
        public float distanceFromTarget;
        public float rotationSpeed = 6;
        public bool canRotate;

        [Header("States")]
        public AIIdle idle;
        public AIChase chase;
        public AIAttack attack;
        public AIAttackStance stance;

        [Header("Debug")]
        public bool debugEnabled;


        private void Awake()
        {
            combatController = GetComponentInChildren<AICombatController>();
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
            {
                isMoving = false;
            }

            if (playerTarget != null)
            {
                distanceFromTarget = Vector3.Distance(transform.position, playerTarget.position);
            }
            else
            {
                isMoving = false;
            }
        }

        private void EnableAIRotation()
        {
            canRotate = true;
        }

        private void DisableAIRotation()
        {
            canRotate = false;
        }

        private void DebugInfo()
        {
            if (playerTarget != null)
            {
                Debug.DrawLine(eyeLevel.position, playerTarget.transform.position, Color.blue);
            }
        }

        private void DisableNavMesh()
        {
            navmesh.isStopped = true;
        }

        private void EnableNavMesh()
        {
            navmesh.isStopped = false;
        }
    }
}
