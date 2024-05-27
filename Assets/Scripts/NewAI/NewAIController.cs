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
        public NewAIState previousAiState;

        //[System.NonSerialized]
        public Transform targetTransform;

        public float distanceToTargetToAttack;
        public float distanceToTargetToChase;

        [System.NonSerialized]
        public Animator animator;

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
            if (previousAiState)
                Destroy(previousAiState);
            if (currentAiState)
                currentAiState.OnExit();
            previousAiState = currentAiState;
            if (newState)
                currentAiState = Instantiate(newState);
            else
                currentAiState = null;
            if (currentAiState)
                currentAiState.controller = this;
        }

        private void FixedUpdate()
        {
            if (!currentAiState) return;
            currentAiState.Execute();
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
    }
}
