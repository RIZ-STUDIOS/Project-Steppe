using ProjectSteppe.Audio;
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

            GetComponent<SoundPlayer>().PlayRandomSound();
        }
    }
}