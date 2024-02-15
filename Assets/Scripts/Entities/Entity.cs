using UnityEngine;

namespace ProjectSteppe.Entities
{
    public class Entity : MonoBehaviour
    {
        private EntityAttacking _entityAttacking;
        private EntityHealth _entityHealth;

        public EntityAttacking EntityAttacking => this.GetComponentIfNull(ref _entityAttacking);
        public EntityHealth EntityHealth => this.GetComponentIfNull(ref _entityHealth);
    }
}
