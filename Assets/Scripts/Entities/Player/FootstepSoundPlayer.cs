using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.Entities.Player
{
    public class FootstepSoundPlayer : MonoBehaviour
    {
        [SerializeField]
        private FootstepSound[] footstepSounds;

        [SerializeField]
        private LayerMask groundLayers;

        [SerializeField]
        private AudioSource audioSource;

        [SerializeField]
        private bool blendStepSounds;

        public void PlayFootstepSound()
        {
            var terrainSounds = GetFootstepSounds();

            if (terrainSounds.Count <= 0) return;

            var terrainSound = terrainSounds[Random.Range(0, terrainSounds.Count)];

            audioSource.PlayOneShot(terrainSound.clips[Random.Range(0, terrainSound.clips.Length)]);
        }

        private List<FootstepSound> GetFootstepSounds()
        {
            List<FootstepSound> sounds = new List<FootstepSound>();

            sounds.AddRange(GetTerrainFootstepSounds());
            sounds.AddRange(GetRendererFootstepSounds());

            return sounds;
        }

        private List<FootstepSound> GetTerrainFootstepSounds()
        {
            List<FootstepSound> sounds = new List<FootstepSound>();

            if (!Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out var hitInfo, 0.5f, groundLayers)) return sounds;

            if (!hitInfo.collider.TryGetComponent<Terrain>(out var terrain)) return sounds;

            var terrainPosition = hitInfo.point - terrain.transform.position;
            var splatMapPosition = new Vector3(
                terrainPosition.x / terrain.terrainData.size.x,
                0,
                terrainPosition.z / terrain.terrainData.size.z
                );

            int x = Mathf.FloorToInt(splatMapPosition.x * terrain.terrainData.alphamapWidth);
            int z = Mathf.FloorToInt(splatMapPosition.z * terrain.terrainData.alphamapHeight);

            var alphaMap = terrain.terrainData.GetAlphamaps(x, z, 1, 1);

            if (!blendStepSounds)
            {
                int primaryIndex = 0;
                for (int i = 1; i < alphaMap.Length; i++)
                {
                    if (alphaMap[0, 0, i] > alphaMap[0, 0, primaryIndex])
                    {
                        primaryIndex = i;
                    }
                }

                foreach (var sound in footstepSounds)
                {
                    if (sound.texture == terrain.terrainData.terrainLayers[primaryIndex].diffuseTexture || sound.texture == null)
                        sounds.Add(sound);
                }
            }
            else
            {
                for (int i = 0; i < alphaMap.Length; i++)
                {
                    if (alphaMap[0, 0, i] > 0)
                    {
                        foreach (var sound in footstepSounds)
                        {
                            if (sound.texture == terrain.terrainData.terrainLayers[i].diffuseTexture || sound.texture == null)
                                sounds.Add(sound);
                        }
                    }
                }
            }

            return sounds;
        }

        private List<FootstepSound> GetRendererFootstepSounds()
        {
            List<FootstepSound> sounds = new List<FootstepSound>();

            if (!Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out var hitInfo, 0.5f, groundLayers)) return sounds;

            if (!hitInfo.collider.TryGetComponent<Renderer>(out var renderer)) return sounds;

            foreach (var sound in footstepSounds)
            {
                if (sound.texture == renderer.material.GetTexture("_Top") || sound.texture == renderer.material.GetTexture("_Sides") || sound.texture == null)
                    sounds.Add(sound);
            }

            return sounds;
        }

        [System.Serializable]
        struct FootstepSound
        {
            public Texture texture;
            public AudioClip[] clips;
        }
    }
}
