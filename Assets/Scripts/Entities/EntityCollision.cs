using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace ProjectSteppe.Entities
{
    public class EntityCollision : MonoBehaviour
    {
        private void Start()
        {
            IgnoreSelfCollisions();
        }

        private void IgnoreSelfCollisions()
        {
            Collider[] entityCollisions = GetComponentsInChildren<Collider>();
            List<Collider> ignoreColliders = new List<Collider>(entityCollisions);

            foreach (var collider in ignoreColliders)
            {
                foreach (var otherCollider in ignoreColliders)
                {
                    Physics.IgnoreCollision(collider, otherCollider, true);
                }
            }
        }
    }
}
