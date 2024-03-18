using StarterAssets;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectSteppe.Entities.Player
{
    public class PlayerAttackController : EntityBehaviour
    {
        private bool firstAttack = true;
        private bool canCombo;

        private StarterAssetsInputs input;
        private Animator animator;
        private PlayerManager playerManager;

        private int animIDAttacking;
        private int animIDNextAttack;
        private int animIDBlocking;

        private bool attacking;
        private bool blocking;

        public UnityEvent onAttack;

        protected override void Awake()
        {
            base.Awake();
            input = GetComponent<StarterAssetsInputs>();
            animator = GetComponentInChildren<Animator>();
            playerManager = GetComponent<PlayerManager>();
        }

        private void Start()
        {
            animIDAttacking = Animator.StringToHash("Attacking");
            animIDNextAttack = Animator.StringToHash("NextAttack");
            animIDBlocking = Animator.StringToHash("Blocking");
        }

        private void Update()
        {
            Attack();
            Block();
        }

        private void Attack()
        {
            if (input.attack)
            {
                if ((!firstAttack && !canCombo) || blocking)
                {
                    input.attack = false;
                    return;
                }
                //Debug.Log("Attacking");
                if (canCombo)
                {
                    animator.SetBool(animIDNextAttack, true);
                }
                onAttack.Invoke();
                animator.SetBool(animIDAttacking, true);
                firstAttack = false;
                input.attack = false;
                attacking = true;
                playerManager.DisableCapability(PlayerCapability.Move);
            }
        }

        private void Block()
        {
            if (input.blocking)
            {
                if (attacking)
                {
                    canCombo = false;
                    RestartAttack();
                    Entity.EntityAttacking.DisableWeaponCollision();
                    animator.SetTrigger("ForcedBlocking");
                    animator.SetTrigger("ForcedExit");
                }
                if (!blocking)
                {
                    Entity.EntityBlock.StartBlock();
                }
            }
            else
            {
                if (blocking)
                {
                    Entity.EntityBlock.EndBlock();
                }
            }
            blocking = input.blocking;
            animator.SetBool(animIDBlocking, input.blocking);
        }

        private void EnableCombo()
        {
            //Debug.Log("Combo enabled");
            canCombo = true;
        }

        private void DisableCombo()
        {
            //Debug.Log("Combo disabled");
            canCombo = false;
            animator.SetBool(animIDAttacking, false);
        }

        public void RestartAttack()
        {
            if (canCombo) return;
            //Debug.Log("Resetting attack");
            firstAttack = true;
            animator.SetBool(animIDAttacking, false);
            animator.SetBool(animIDNextAttack, false);
            attacking = false;
            playerManager.EnableCapability(PlayerCapability.Move);
            playerManager.EnableCapability(PlayerCapability.Rotate);
        }
    }
}
