using UnityEngine;

namespace ProjectSteppe.Entities
{
    public class Entity : MonoBehaviour
    {
        private EntityAttacking _entityAttacking;
        private EntityHealth _entityHealth;
        private EntityBlock _entityBlock;

        public EntityHealth EntityHealth => this.GetComponentIfNull(ref _entityHealth);
        public EntityAttacking EntityAttacking => this.GetComponentIfNull(ref _entityAttacking);
        public EntityBlock EntityBlock => this.GetComponentIfNull(ref _entityBlock);
    }
}
