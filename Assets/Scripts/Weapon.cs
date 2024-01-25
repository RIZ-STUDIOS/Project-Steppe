using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponScriptableObject weapon;
        [SerializeField] private List<Collider> collisions;
        private GameObject player;

        private void Awake()
        {
            //player = GameObject.FindGameObjectWithTag("Player").GetComponent<StarterAssetsInputs>();
            //one way of hooking it up to control scheme, doesn't recognise atm since diff folder
        }

        public void WeaponEnable()
        {
            WeaponSwingAction(true);
        }

        public void WeaponDisable()
        {
            WeaponSwingAction(false);
        }

        private void WeaponSwingAction(bool state)
        {
            foreach (Collider col in collisions)
            {
                col.enabled = state;
            }
        }
    }
}
