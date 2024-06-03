using UnityEngine;
using UnityEngine.Events;

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

        [SerializeField]
        private ParticleSystem[] unblockableFX;

        public bool IsBlocking => blocking;

        public UnityEvent OnBlockStart;
        public UnityEvent OnBlockEnd;

        public UnityEvent OnBlockAttack;
        public UnityEvent OnParryAttack;

        [ContextMenu("Start Block")]
        public void StartBlock()
        {
            blocking = true;
            OnBlockStart?.Invoke();
        }

        public void EndBlock()
        {
            blocking = false;
            Debug.Log($"Blocked for {blockTime} seconds");
            blockTime = 0;
            OnBlockEnd?.Invoke();
        }

        public bool IsPerfectBlock()
        {
            return blocking && blockTime <= perfectBlockTimeWindow;
        }

        private void Update()
        {
            if (blocking)
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

        public void ShowUnblockFX()
        {
            foreach(var fx in unblockableFX)
            {
                fx.Play();
            }
        }
    }
}
