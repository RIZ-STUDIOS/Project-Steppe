using ProjectSteppe.AI.States;
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

        private Entity _targetEntity;

        public Entity targetEntity
        {
            get
            {
                return _targetEntity;
            }
            set
            {
                if (value)
                {
                    previousPosition = value.transform.position;
                    targetFuturePosition = previousPosition;
                }
                _targetEntity = value;
            }
        }

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

        private Vector3 previousPosition;
        private Vector3 targetFuturePosition;

        [SerializeField]
        private float futurePositionAccuracy = 1;

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
            if (targetEntity)
            {
                if (Time.timeScale > 0)
                {
                    var diff = targetEntity.transform.position - previousPosition;
                    var thisFrameTargetFuturePosition = targetEntity.transform.position + ((diff * (1 / Time.deltaTime)) * futurePositionAccuracy);
                    targetFuturePosition = Vector3.MoveTowards(targetFuturePosition, thisFrameTargetFuturePosition, 20 * Time.deltaTime);
                }
                previousPosition = targetEntity.transform.position;
                if (rotateTowardsTarget)
                {
                    var state = currentAiState as AIAttackHandlerState;
                    RotateTowards((state != null && state.InAttack()) ? targetFuturePosition : targetEntity.transform.position);
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(targetFuturePosition, new Vector3(1.5f, 1.5f, 1.5f));
        }

        private void OnDisable()
        {
            if (!currentAiState) return;
            currentAiState.OnDisable();
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
            if (!targetEntity) return;
            SetPathTo(targetFuturePosition);
        }

        public void RotateTowardsTarget()
        {
            if (!targetEntity) return;
            RotateTowards(targetFuturePosition);
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

        public void TurnOnUnblockable()
        {
            AIEntity.EntityAttacking.CurrentWeapon.ShowUnblockable();
        }

        public void TurnOffUnblockable()
        {
            AIEntity.EntityAttacking.CurrentWeapon.HideUnblockable();
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
