using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.Entities
{
    public class EntityBlock : EntityBehaviour
    {
        [SerializeField]
        private float perfectBlockTimeWindow = 0.12f;

        private float blockTime;

        private bool blocking;

        public bool IsBlocking => blocking;

        public void StartBlock()
        {
            blocking = true;
        }

        public void EndBlock()
        {
            blocking = false;
            Debug.Log($"Blocked for {blockTime} seconds");
            blockTime = 0;
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
    }
}
