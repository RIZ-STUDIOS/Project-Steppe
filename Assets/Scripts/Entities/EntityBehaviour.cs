using UnityEngine;

namespace ProjectSteppe.Entities
{
    [AddComponentMenu("")]
    public class EntityBehaviour : MonoBehaviour
    {
        private Entity entity;

        public Entity Entity => entity;

        protected virtual void Awake()
        {
            entity = GetComponent<Entity>();
        }
    }
}
