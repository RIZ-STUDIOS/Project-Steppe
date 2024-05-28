using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe
{
    public class FollowObject : MonoBehaviour
    {
        [SerializeField]
        private Transform followTransform;

        private Vector3 pos, fw, up;

        private void Start()
        {
            ChangeFakeParent(followTransform);
        }

        private void Update()
        {
            var newpos = followTransform.transform.TransformPoint(pos);
            var newfw = followTransform.transform.TransformDirection(fw);
            var newup = followTransform.transform.TransformDirection(up);
            var newrot = Quaternion.LookRotation(newfw, newup);
            transform.position = newpos;
            transform.rotation = newrot;
        }

        public void ChangeFakeParent(Transform parent)
        {
            followTransform = parent;
            pos = followTransform.transform.InverseTransformPoint(transform.position);
            fw = followTransform.transform.InverseTransformDirection(transform.forward);
            up = followTransform.transform.InverseTransformDirection(transform.up);
        }
    }
}
