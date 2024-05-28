using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectSteppe.AI
{
    public class NewAIController : MonoBehaviour
    {
        private NavMeshAgent navMeshAgent;

        public NavMeshAgent NavMeshAgent => navMeshAgent;

        private int playerHitsTotal;

        private int playerHits;

        public int PlayerHitsTotal => playerHitsTotal;
        public int PlayerHits => playerHits;

        [SerializeField]
        private NewAIState currentAiState;

        [System.NonSerialized]
        public Transform targetTransform;

        public float distanceToTargetToAttack;
        public float distanceToTargetToChase;

        [System.NonSerialized]
        public Animator animator;

        [Range(0.0f, 0.3f)]
        public float RotationSmoothTime = 0.12f;

        private float targetRotation;
        private float _rotationVelocity;

        private bool rotateTowardsTarget;

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

        public void SwitchAIState(NewAIState newState)
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
        }

        private void Update()
        {
            if (rotateTowardsTarget && targetTransform)
            {
                RotateTowards(targetTransform);
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
            SetPathTo(targetTransform);
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
    }
}
