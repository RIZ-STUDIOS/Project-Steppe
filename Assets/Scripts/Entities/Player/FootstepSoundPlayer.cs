using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.Entities.Player
{
    public class FootstepSoundPlayer : MonoBehaviour
    {

        [System.Serializable]
        struct FootstepSound
        {
            public Texture texture;
            public AudioClip[] clips;
        }
    }
}
