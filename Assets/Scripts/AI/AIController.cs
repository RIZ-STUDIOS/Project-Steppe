using ProjectSteppe.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectSteppe.AI
{
    public class AIController : MonoBehaviour
    {
        private NavMeshAgent navMeshAgent;

        public NavMeshAgent NavMeshAgent => navMeshAgent;

        private int playerHitsTotal;

        private int playerHits;

        public int PlayerHitsTotal => playerHitsTotal;
        public int PlayerHits => playerHits;

        [SerializeField]
        private AIState currentAiState;

        [System.NonSerialized]
        public AITarget targetTransform;

        public float distanceToTargetToAttack;
        public float distanceToTargetToChase;

        [System.NonSerialized]
        public Animator animator;

        [Range(0.0f, 0.3f)]
        public float RotationSmoothTime = 0.12f;

        private float targetRotation;
        private float _rotationVelocity;

        private bool rotateTowardsTarget;

        [System.NonSerialized]
        public bool playerAttacking;

        private bool committedToAttack;

        public bool CommittedToAttack => committedToAttack;

        private Entity aiEntity;
        public Entity AIEntity => this.GetComponentIfNull(ref aiEntity);

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            SwitchAIState(currentAiState);
        }

        public void IncrementPlayerHits()
        {
            playerHitsTotal++;
            playerHits++;
        }

        public void ResetPlayerHits()
        {
            playerHits = 0;
        }

        public void SwitchAIState(AIState newState)
        {
            if (currentAiState)
                currentAiState.OnExit();
            if (newState)
                currentAiState = Instantiate(newState);
            else
                currentAiState = null;
            if (currentAiState)
            {
                currentAiState.controller = this;
                currentAiState.OnEnter();
            }
        }

        private void FixedUpdate()
        {
            if (!currentAiState) return;
            currentAiState.Execute();
            if (playerAttacking)
            {
                playerAttacking = false;
            }
        }

        private void Update()
        {
            if (rotateTowardsTarget && targetTransform)
            {
                RotateTowards(targetTransform.transform);
            }
        }

        public void SetPathTo(Transform target)
        {
            SetPathTo(target.position);
        }

        public void SetPathTo(Vector3 position)
        {
            var path = new NavMeshPath();
            navMeshAgent.CalculatePath(position, path);
            navMeshAgent.SetPath(path);
        }

        public void SetPathToTarget()
        {
            if (!targetTransform) return;
            SetPathTo(targetTransform.transform);
        }

        public void RotateTowards(Transform transform)
        {
            RotateTowards(transform.position);
        }

        public void RotateTowards(Vector3 position)
        {
            var rot = Quaternion.LookRotation(position - transform.position).eulerAngles;
            targetRotation = rot.y;

            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref _rotationVelocity,
                        RotationSmoothTime);
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        public void EnableRotationTowardsTarget()
        {
            rotateTowardsTarget = true;
        }

        public void DisableRotationTowardsTarget()
        {
            rotateTowardsTarget = false;
        }

        public void CommitToAttack()
        {
            committedToAttack = true;
        }

        public void UncommitToAttack()
        {
            committedToAttack = false;
        }

        // Animation
        public void EnableNavMesh()
        {
            navMeshAgent.isStopped = false;
        }

        public void DisableNavMesh()
        {
            navMeshAgent.isStopped = true;
        }
    }
}
