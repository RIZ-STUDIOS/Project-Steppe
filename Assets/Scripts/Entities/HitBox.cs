using ProjectSteppe.Entities;
using UnityEngine;

namespace ProjectSteppe
{
    public class HitBox : MonoBehaviour
    {
        private Entity parentEntity;

        public Entity ParentEntity => parentEntity;

        private void Awake()
        {
            gameObject.layer = 17;
            parentEntity = GetComponentInParent<Entity>();
            if (parentEntity == null)
            {
                Debug.LogWarning($"Hitbot does not have Entity Parent", this);
            }
        }

        public bool IsValidHit(Entity other)
        {
            if (!parentEntity) return false;
            return other != parentEntity;
        }
    }
}
