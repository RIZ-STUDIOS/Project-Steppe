using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.Entities
{
    public class EntityBlock : EntityBehaviour
    {
        [SerializeField]
        private float perfectBlockTimeWindow = 0.5f;

        private float blockTime;

        private bool blocking;

        [SerializeField]
        [ColorUsage(false)]
        private Color normalBlockColor;

        [SerializeField]
        [ColorUsage(false)]
        private Color perfectBlockColor;

        [SerializeField]
        private ParticleSystem[] blockFX;

        public bool IsBlocking => blocking;

        public System.Action onBlockStart;
        public System.Action onBlockEnd;

        [ContextMenu("Start Block")]
        public void StartBlock()
        {
            blocking = true;
            onBlockStart?.Invoke();
        }

        public void EndBlock()
        {
            blocking = false;
            Debug.Log($"Blocked for {blockTime} seconds");
            blockTime = 0;
            onBlockEnd?.Invoke();
        }

        public bool IsPerfectBlock()
        {
            return blocking && blockTime <= perfectBlockTimeWindow;
        }

        private void Update()
        {
            if(blocking)
            {
                blockTime += Time.deltaTime;
            }
        }

        public void ChangeBlockColor(bool isPerfect)
        {
            foreach (var fx in blockFX)
            {
                var main = fx.main;
                var color = isPerfect ? perfectBlockColor : normalBlockColor;
                color.a = 1;
                main.startColor = color;
            }
        }

        public void PlayBlockFX()
        {
            foreach (var fx in blockFX)
            {
                fx.Play();
            }
        }
    }
}
