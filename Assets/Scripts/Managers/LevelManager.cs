using ProjectSteppe.Entities.Player;
using ProjectSteppe.Interactions.Interactables;
using ProjectSteppe.Managers;
using RicTools.Managers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectSteppe.Managers
{
    public class LevelManager : GenericManager<LevelManager>
    {
        public CheckpointInteractable[] checkpoints;

        public int LevelIndex { get; private set; } = -1;

        [SerializeField]
        private PlayerManager player;

        protected override void Awake()
        {
            base.Awake();

            if (LevelIndex == -1 || LevelIndex != SceneManager.GetActiveScene().buildIndex)
            {
                LevelIndex = SceneManager.GetActiveScene().buildIndex;
            }

            InitCheckpoints();

            if (
                SaveManager.CurrentSave.currentCheckpointIndex >= checkpoints.Length ||
                SaveManager.CurrentSave.currentCheckpointIndex < 0
               )
            {
                SaveManager.CurrentSave.currentCheckpointIndex = 0;
            }

            player.transform.SetPositionAndRotation
                (
                    checkpoints[SaveManager.CurrentSave.currentCheckpointIndex].spawnPoint.transform.position,
                    checkpoints[SaveManager.CurrentSave.currentCheckpointIndex].spawnPoint.transform.rotation
                );

            SaveManager.SaveGame();
        }

        private void InitCheckpoints()
        {
            for (int i = 0; i < checkpoints.Length; i++)
            {
                var checkpoint = checkpoints[i];
                checkpoint.levelIndex = i;
                checkpoint.OnCheckpointActivate.AddListener(SaveActivatedCheckpoint);

                if (!SaveManager.CurrentSave.checkpointStates.ContainsKey(checkpoint.id))
                {
                    SaveManager.CurrentSave.checkpointStates.Add(checkpoint.id, checkpoint.StartDiscovered);
                }

                if (!SaveManager.CurrentSave.checkpointStates[checkpoint.id] && checkpoint.StartDiscovered)
                {
                    SaveManager.CurrentSave.checkpointStates[checkpoint.id] = true;
                }

                checkpoint.StartDiscovered = SaveManager.CurrentSave.checkpointStates[checkpoint.id];

                checkpoint.DiscoveredQuery();
            }
        }

        private void SaveActivatedCheckpoint(CheckpointInteractable checkpoint)
        {
            SaveManager.CurrentSave.currentSceneIndex = LevelIndex;
            SaveManager.CurrentSave.currentCheckpointIndex = checkpoint.levelIndex;
            SaveManager.CurrentSave.checkpointStates[checkpoint.id] = true;

            SaveManager.SaveGame();
        }
    }
}
