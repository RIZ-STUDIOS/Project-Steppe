using UnityEngine;
using UnityEngine.Events;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
    public class StarterAssetsInputs : MonoBehaviour
    {
        public bool respondToData = true;

        [Header("Character Input Values")]
        public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool sprint;
        public bool dash;
        public bool attack;
        public bool targetLock;
        public bool blocking;

        [Header("Movement Settings")]
        public bool analogMovement;

        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;
        public bool cursorInputForLook = true;

        [Space]
        public UnityEvent OnInteraction;

#if ENABLE_INPUT_SYSTEM
        public void OnMove(InputValue value)
        {
            if (!respondToData) return;
            MoveInput(value.Get<Vector2>());
        }

        public void OnLook(InputValue value)
        {
            if (!respondToData) return;
            if (cursorInputForLook)
            {
                LookInput(value.Get<Vector2>());
            }
        }

        public void OnBlock(InputValue value)
        {
            if (!respondToData) return;
            BlockInput(value.isPressed);
        }

        public void OnJump(InputValue value)
        {
            if (!respondToData) return;
            JumpInput(value.isPressed);
        }

        public void OnSprint(InputValue value)
        {
            if (!respondToData) return;
            SprintInput(value.isPressed);
        }

        public void OnDash(InputValue value)
        {
            if (!respondToData) return;
            DashInput(value.isPressed);
        }

        public void OnAttack(InputValue value)
        {
            if (!respondToData) return;
            AttackInput(value.isPressed);
        }

        public void OnLock(InputValue value)
        {
            if (!respondToData) return;
            LockInput(value.isPressed);
        }

        public void OnInteract(InputValue value)
        {
            if (!respondToData) return;
            OnInteraction.Invoke();
        }
#endif


        public void MoveInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection;
        }

        public void LookInput(Vector2 newLookDirection)
        {
            look = newLookDirection;
        }

        public void JumpInput(bool newJumpState)
        {
            jump = newJumpState;
        }

        public void SprintInput(bool newSprintState)
        {
            sprint = newSprintState;
        }

        public void DashInput(bool newDashState)
        {
            dash = newDashState;
        }

        public void AttackInput(bool newAttackState)
        {
            attack = newAttackState;
        }

        public void LockInput(bool newLockState)
        {
            targetLock = newLockState;
        }

        public void BlockInput(bool newBlockState)
        {
            blocking = newBlockState;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            SetCursorState(cursorLocked);
        }

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }

        public void ResetInputs()
        {
            MoveInput(Vector2.zero);
            LookInput(Vector2.zero);
            DashInput(false);
        }
    }

}