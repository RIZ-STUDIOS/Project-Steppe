using ProjectSteppe.Managers;
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

        private float playerMaxHealth;

        private void Start()
        {
            if (SaveHandler.CurrentSave.playerStatLevel > 1)
                totalStatLevel = SaveHandler.CurrentSave.playerStatLevel;

            InitStatistics();

            SaveHandlerDetails();

            playerMaxHealth = GetComponent<EntityHealth>().MaxHealth;

            ApplyStatistics();
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

        public void ApplyStatistics()
        {
            var toughness = statistics.Find(t => t.type == PlayerStatisticType.Toughness);
            var playerHealth = GetComponent<EntityHealth>();
            playerHealth.ChangeMaxHealth(playerMaxHealth * (1 + (0.03f * (toughness.Level - 1))));

            var precision = statistics.Find(p => p.type == PlayerStatisticType.Precision);
            var playerAttack = GetComponent<EntityAttacking>();
            playerAttack.damageMultiplier = 1 + (0.03f * (precision.Level - 1));

            var swiftness = statistics.Find(sw => sw.type == PlayerStatisticType.Swiftness);
            var playerMovement = GetComponent<PlayerMovementController>();
            playerMovement.speedMultiplier = 1 + (0.03f * (swiftness.Level - 1));

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
