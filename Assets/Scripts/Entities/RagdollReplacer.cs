using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe
{
    public class RagdollReplacer : MonoBehaviour
    {
        [SerializeField]
        private GameObject ragdollPrefab;

        [SerializeField]
        private Transform hipsTransform;

        public void Replace()
        {
            var ragdoll = Instantiate(ragdollPrefab);
            ragdoll.transform.position = transform.position;
            ragdoll.transform.rotation = transform.rotation;

            CopyTransform(hipsTransform, ragdoll.GetComponentInChildren<Rigidbody>().transform);

            Destroy(gameObject);
        }

        private void CopyTransform(Transform current, Transform ragdollTransform)
        {
            ragdollTransform.position = current.position;
            ragdollTransform.rotation = current.rotation;
            for (int i = 0; i < current.childCount; i++)
            {
                CopyTransform(current.GetChild(i), ragdollTransform.GetChild(i));
            }
        }
    }
}
