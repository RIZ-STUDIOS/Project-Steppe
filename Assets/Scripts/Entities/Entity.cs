using UnityEngine;

namespace ProjectSteppe.Entities
{
    [RequireComponent(typeof(EntityAttacking))]
    [RequireComponent(typeof(EntityHealth))]
    [RequireComponent(typeof(EntityBlock))]
    [RequireComponent(typeof(EntityCollision))]
    public class Entity : MonoBehaviour
    {
        private EntityAttacking _entityAttacking;
        private EntityHealth _entityHealth;
        private EntityBlock _entityBlock;

        public EntityAttacking EntityAttacking => this.GetComponentIfNull(ref _entityAttacking);
        public EntityHealth EntityHealth => this.GetComponentIfNull(ref _entityHealth);
        public EntityBlock EntityBlock => this.GetComponentIfNull(ref _entityBlock);
    }
}
