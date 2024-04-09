using ProjectSteppe.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe
{
    public class AIBlock : MonoBehaviour
    {
        private EntityHealth health;
        private AIAnimator animator;
        private AIAttackStatus attackStatus;
        private Entity entity;

        private void Awake()
        {
            attackStatus = GetComponent<AIAttackStatus>();
            animator = GetComponent<AIAnimator>();
            health = GetComponent<EntityHealth>();
            entity = GetComponent<Entity>();

            health.onHit.AddListener(BlockQuery);
        }

        private void BlockQuery()
        {
            if (animator.hyperArmour) return;
            if (animator.attackCommitted)
            {
                animator.animController.SetTrigger("ForceAnimation");
                animator.animController.SetTrigger("Hit");
            }
            else
            {
                animator.animController.SetTrigger("ForceAnimation");
                animator.animController.SetTrigger("Block");

                var damage = GetComponent<AIController>().playerTarget.GetComponent<EntityAttacking>().currentAttack.healthDamage;

                entity.EntityHealth.HealHealth(damage);
            }

            var ai = GetComponent<AICombatController>().currentRecoveryTime = 0f;
        }
    }
}
