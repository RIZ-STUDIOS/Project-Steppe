using ProjectSteppe.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.Entities.Player
{
    public class PlayerStatisticHandler : MonoBehaviour
    {
        public const int BASE_STATISTIC_COST = 80;

        public List<PlayerStatistic> statistics = new();
        public int totalStatLevel;

        private void Start()
        {
            if (SaveHandler.CurrentSave.playerStatLevel > 1)
                totalStatLevel = SaveHandler.CurrentSave.playerStatLevel;

            InitStatistics();

            SaveHandlerDetails();
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
        }

        public void SaveHandlerDetails()
        {
            SaveHandler.CurrentSave.playerStatistics = statistics;
            SaveHandler.CurrentSave.playerStatLevel = totalStatLevel;
            SaveHandler.SaveGame();
        }
    }

    [System.Serializable]
    public class PlayerStatistic
    {
        public PlayerStatisticType type;
        private int _level;
        public int Level
        {
            get { return _level; }
            set
            {
                int diff = value - _level;
                _level = value;
                OnValueChange?.Invoke(diff);
            }
        }

        System.Action<int> OnValueChange;

        public PlayerStatistic(PlayerStatisticType type, int level)
        {
            this.type = type;
            _level = level;
        }
    }

    public enum PlayerStatisticType
    {
        Precision,
        Swiftness,
        Toughness
    }
}
