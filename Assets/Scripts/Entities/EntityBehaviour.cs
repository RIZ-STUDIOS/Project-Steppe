using UnityEngine;

namespace ProjectSteppe.Entities
{
    [RequireComponent(typeof(Entity))]
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
