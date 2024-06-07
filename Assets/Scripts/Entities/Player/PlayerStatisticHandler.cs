using ProjectSteppe.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.Entities.Player
{
    public class PlayerStatisticHandler : MonoBehaviour
    {
        public List<PlayerStatistic> statistics = new();

        private void Start()
        {
            InitStatistics();
        }

        private void InitStatistics()
        {
            var statTypes = Enum.GetValues(typeof(PlayerStatisticType));
            foreach (var stat in statTypes)
            {
                var statType = (PlayerStatisticType)stat;

                var foundStat = SaveHandler.CurrentSave.playerStatistics.Find(p => p.type == statType);

                if (foundStat != null)
                {
                    statistics.Add(foundStat);
                }
                else
                {
                    statistics.Add(new PlayerStatistic(statType, 1));
                }
            }

            SaveHandler.CurrentSave.playerStatistics = statistics;
            SaveHandler.SaveGame();
        }
    }

    [System.Serializable]
    public class PlayerStatistic
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
