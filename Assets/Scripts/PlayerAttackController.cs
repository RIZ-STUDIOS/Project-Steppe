using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

namespace ProjectSteppe
{
    public class PlayerAttackController : MonoBehaviour
    {
        ThirdPersonController thirdPersonController;
        private StarterAssetsInputs _input;
        private int _animIDAttacking;
        private Animator _animator;

        [SerializeField] private GameObject weapon;
        private float attackDuration = 0.5f;

        private void Awake()
        {
            thirdPersonController = GetComponent<ThirdPersonController>();
        }

        private void Start()
        {
            _input = GetComponent<StarterAssetsInputs>();

            TryGetComponent(out _animator);
            _animIDAttacking = Animator.StringToHash("Attacking");
        }

        private void Update()
        {
            Attack();
        }

        private void Attack()
        {
            if (!thirdPersonController.Grounded)
            {
                if(_input.attack) _input.attack = false;
            }

            if (_input.attack && thirdPersonController.canMove)
            {
                _animator.SetBool(_animIDAttacking, true);
                thirdPersonController.canMove = false;
                weapon.GetComponent<Weapon>().ToggleAttack(attackDuration); // or change to enable/disable
            }
            else if (_input.attack)
            {
                _input.attack = false;
            }
        }
    }
}
