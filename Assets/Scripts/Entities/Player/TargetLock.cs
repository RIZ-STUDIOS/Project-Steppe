using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

namespace ProjectSteppe.Entities.Player
{
    public class TargetLock : MonoBehaviour
    {
        private StarterAssetsInputs _input;

        private Transform targetTransform;
        private ThirdPersonController thirdPersonController;

        [SerializeField]
        private Transform bossTransform;

        [SerializeField]
        private CinemachineVirtualCamera thirdPersonVirtualCamera;

        [SerializeField]
        private CinemachineVirtualCamera targetLockVirtualCamera;

        [System.NonSerialized]
        public bool lockOn;

        private bool justLocked;

        private void Awake()
        {
            _input = GetComponent<StarterAssetsInputs>();
            thirdPersonController = GetComponent<ThirdPersonController>();
            SetLockTarget(bossTransform);
            SetLockOn(false);
        }

        private void Update()
        {
            if(_input.targetLock && justLocked)
            {
                _input.targetLock = false;
            }

            if(!_input.targetLock && justLocked)
            {
                justLocked = false;
            }

            if(_input.targetLock && !justLocked)
            {
                SetLockOn(!lockOn);
                justLocked = true;
            }

            if (lockOn)
            {
                Vector3 dir = targetTransform.position - transform.position;
                var d = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 5 * Time.deltaTime).eulerAngles;
                transform.rotation = Quaternion.Euler(0, d.y, 0);
            }
        }

        public void SetLockOn(bool lockOn)
        {
            this.lockOn = lockOn;
            thirdPersonVirtualCamera.enabled = !lockOn;
            targetLockVirtualCamera.enabled = lockOn;
            thirdPersonController.strafe = lockOn;
        }

        public void SetLockTarget(Transform target)
        {
            targetTransform = target;
            targetLockVirtualCamera.LookAt = target;
        }
    }
}
