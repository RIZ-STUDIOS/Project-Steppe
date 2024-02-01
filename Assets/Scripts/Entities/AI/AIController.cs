using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe
{
    public class AIController : MonoBehaviour
    {
        [Header("State Settings")]
        [SerializeField] private AIState currentState;

        [Header("Other")]
        public AICombatController combatController;
        public Transform eyeLevel;

        public Transform playerTarget;

        private void Awake()
        {
            combatController = GetComponent<AICombatController>();
        }

        private void FixedUpdate()
        {
            UpdateState();
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
    }
}
