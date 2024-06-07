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
        //[ColorUsage(false)]
        private Color normalBlockColor;

        [SerializeField]
        //[ColorUsage(false)]
        private Color perfectBlockColor;

        [SerializeField]
        private ParticleSystem[] blockFX;

        public bool IsBlocking => blocking;

        public UnityEvent OnBlockStart;
        public UnityEvent OnBlockEnd;

        public UnityEvent OnBlockAttack;
        public UnityEvent OnParryAttack;

        public float parryGracePeriod = 0.2f;
        private bool parryGrace;
        private float parryTime;

        protected override void Awake()
        {
            base.Awake();
            OnParryAttack.AddListener(EnableParryGrace);
        }

        private void EnableParryGrace()
        {
            parryGrace = true;
        }

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
            return blocking && blockTime <= perfectBlockTimeWindow || parryGrace;
        }

        private void Update()
        {
            if (blocking)
            {
                blockTime += Time.deltaTime;
            }

            if (parryGrace)
            {
                parryTime += Time.deltaTime;

                if (parryTime >= parryGracePeriod)
                {
                    parryTime = 0;
                    parryGrace = false;
                }
            }
        }

        public void ChangeBlockColor(bool isPerfect)
        {
            foreach (var fx in blockFX)
            {
                var renderer = fx.GetComponent<ParticleSystemRenderer>();
                var color = isPerfect ? perfectBlockColor : normalBlockColor;
                color.a = 1;
                renderer.sharedMaterials[1].SetColor("_EmissionColor", color);
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
