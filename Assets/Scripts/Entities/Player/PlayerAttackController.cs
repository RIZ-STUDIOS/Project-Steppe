using StarterAssets;
using UnityEngine;

namespace ProjectSteppe.Entities.Player
{
    [RequireComponent(typeof(Entity))]
    public class PlayerAttackController : MonoBehaviour
    {
        private ThirdPersonController thirdPersonController;
        private StarterAssetsInputs _input;
        private int _animIDAttacking;
        private Animator _animator;

        private Entity entity;

        private bool canCombo;

        private void Awake()
        {
            entity = GetComponent<Entity>();
            thirdPersonController = GetComponent<ThirdPersonController>();
        }

        private void Start()
        {
            _input = GetComponent<StarterAssetsInputs>();

            _animator = GetComponent<Animator>();
            _animIDAttacking = Animator.StringToHash("Attacking");
        }

        private void Update()
        {
            Attack();
        }

        private void Attack()
        {
            if (!thirdPersonController.Grounded || thirdPersonController.dashing)
            {
                if (_input.attack) _input.attack = false;
            }

            if (_input.attack && thirdPersonController.canMove)
            {
                _animator.SetBool(_animIDAttacking, true);
                thirdPersonController.canMove = false;
            }
            else if (_input.attack)
            {
                _input.attack = false;
            }
        }

        // Called in Attack animation
        private void EnableWeapon()
        {
            if (!entity.CurrentWeapon) return;

            entity.CurrentWeapon.EnableColliders();
        }

        // Called in Attack animation
        public void DisableWeapon()
        {
            if (!entity.CurrentWeapon) return;

            entity.CurrentWeapon.DisableColliders();
        }

        // Called in Attack animation
        private void DisableRotation()
        {
            thirdPersonController.canRotate = false;
        }

        // Called in Attack animation
        private void StartAttack()
        {
            thirdPersonController.RotationSmoothTime = 0.03f;
        }
    }
}
