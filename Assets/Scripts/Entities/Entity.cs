using ProjectSteppe.UI;
using UnityEngine;

namespace ProjectSteppe.Entities
{
    public class Entity : MonoBehaviour
    {
        private EntityAttacking _entityAttacking;
        private EntityHealth _entityHealth;
        private EntityBlock _entityBlock;
        private EntityDetailsUI _entityDetailsUI;

        public EntityHealth EntityHealth => this.GetComponentIfNull(ref _entityHealth);
        public EntityAttacking EntityAttacking => this.GetComponentIfNull(ref _entityAttacking);
        public EntityBlock EntityBlock => this.GetComponentIfNull(ref _entityBlock);
        public EntityDetailsUI EntityDetails => this.GetComponentIfNull(ref _entityDetailsUI);
    }
}
