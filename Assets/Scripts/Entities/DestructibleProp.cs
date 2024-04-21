using ProjectSteppe.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe
{
    public class DestructibleProp : MonoBehaviour
    {
        public void ToppleProp(Vector3 force)
        {
            var rb = GetComponent<Rigidbody>();
            rb.isKinematic = false;

            rb.AddForce(force, ForceMode.Impulse);
        }
    }
}