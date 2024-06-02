using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace ProjectSteppe.Entities.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField]
        private Weapon playerWeapon;

        [SerializeField]
        private PlayerMovementController playerMovement;

        [SerializeField]
        private EntityBlock playerBlock;

        [SerializeField]
        private Animator animator;

        [SerializeField]
        private TargetLock targetLock;

        #region Rigging Variables

        [SerializeField]
        private MultiAimConstraint headConstraint;

        [SerializeField]
        private MultiAimConstraint spineConstraint;

        [SerializeField]
        private Transform headTarget;

        [SerializeField]
        private Transform spineTarget;

        #endregion Rigging Variables

        #region Movement IDs
        private int _animIDJump;
        private int _animIDEndJump;
        private int _animIDDash;
        private int _animIDMotionSpeed;
        private int _animIDSpeed;
        private int _animIDVelocityX;
        private int _animIDVelocityY;
        #endregion Movement IDs

        #region Combat IDs
        private int _animIDPerfectBlock;
        private int _animIDDeflectIndex;
        private int _layerIndexBase;
        private int _layerIndexBlock;
        #endregion Combat IDs

        private int currentDeflectIndex;

        [SerializeField]
        private float parryAnimReset = 0.5f;

        private bool isLocked;

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            playerMovement = GetComponent<PlayerMovementController>();
            playerBlock = GetComponent<EntityBlock>();
            targetLock = GetComponent<TargetLock>();
        }

        private void Start()
        {
            _animIDJump = Animator.StringToHash("Jump");
            _animIDEndJump = Animator.StringToHash("EndJump");
            //_animIDDash = Animator.StringToHash("Dashing");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDVelocityX = Animator.StringToHash("MoveDirectionX");
            _animIDVelocityY = Animator.StringToHash("MoveDirectionY");
            _animIDPerfectBlock = Animator.StringToHash("PerfectBlock");
            _animIDDeflectIndex = Animator.StringToHash("DeflectIndex");
            _layerIndexBase = animator.GetLayerIndex("Base");
            _layerIndexBlock = animator.GetLayerIndex("Block");

            var attack = GetComponent<EntityAttacking>();
            playerWeapon = attack.CurrentWeapon;

            playerMovement.onMoveAnimator += OnMoveInput;
            playerMovement.onGround.AddListener(OnGround);
            playerMovement.onJump.AddListener(OnJumpEvent);
            playerMovement.onDashStart.AddListener(OnDashStart);
            playerMovement.onDashEnd.AddListener(OnDashEnd);
            playerWeapon.onParry += OnParry;
            playerBlock.OnBlockStart.AddListener(OnBlockStart);
            playerBlock.OnBlockEnd.AddListener(OnBlockEnd);
        }

        #region Movement

        private void OnMoveInput(float animationBlend, float inputMagnitude, float velocityX, float velocityY)
        {
            animator.SetFloat(_animIDSpeed, animationBlend);
            animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
            animator.SetFloat(_animIDVelocityX, velocityX);
            animator.SetFloat(_animIDVelocityY, velocityY);
        }

        private void OnGround()
        {
            animator.SetTrigger(_animIDEndJump);
        }

        private void OnJumpEvent()
        {
            animator.ResetTrigger(_animIDEndJump);
            animator.SetTrigger(_animIDJump);
        }

        private void OnDashStart()
        {
            animator.SetTrigger("ForceAnimation");
            animator.SetTrigger("Roll");
            //animator.SetBool(_animIDDash, true);
        }

        private void OnDashEnd()
        {
            //animator.SetBool(_animIDDash, false);
        }

        #endregion Movement

        #region Combat

        private void OnParry()
        {
            spineConstraint.weight = 0;

            //animator.SetLayerWeight(_layerIndexBlock, 0);
            //animator.SetFloat(_animIDDeflectIndex, currentDeflectIndex);
            //animator.SetBool(_animIDPerfectBlock, true);

            //currentDeflectIndex = currentDeflectIndex == 0 ? 1 : 0;

            //StartCoroutine(ParryReset());
        }

        private void OnBlockStart()
        {
            spineConstraint.weight = 1;
            //headConstraint.weight = 1;
        }

        private void OnBlockEnd()
        {
            spineConstraint.weight = 0;
            //headConstraint.weight = 0;
        }

        private IEnumerator ParryReset()
        {
            yield return new WaitForSeconds(parryAnimReset);
            animator.SetBool(_animIDPerfectBlock, false);
            animator.SetLayerWeight(_layerIndexBlock, 1);
        }

        #endregion Combat

        #region Rigging

        public void ToggleRigging()
        {
            return;
            isLocked = !isLocked;

            if (isLocked)
            {
                headConstraint.data.sourceObjects.Add(new WeightedTransform(targetLock.lookAtTransform, 1));

                headConstraint.weight = 1;
            }
            else
            {
                headConstraint.data.sourceObjects.RemoveAt(0);

                headConstraint.weight = 0;
            }
        }

        #endregion Rigging
    }
}
