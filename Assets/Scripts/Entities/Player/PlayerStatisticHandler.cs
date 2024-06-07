using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.Entities.Player
{
    public class PlayerStatisticHandler : MonoBehaviour
    {
        public List<PlayerStatistic> statistics = new();

        private void Awake()
        {
            
        }
    }

    [System.Serializable]
    public struct PlayerStatistic
    {
        public PlayerStatisticType type;
        public int level;

        public PlayerStatistic(PlayerStatisticType type, int level)
        {
            this.type = type;
            this.level = level;
        }
    }

    public enum PlayerStatisticType
    {
        Precision,
        Swiftness,
        Toughness
    }
}
