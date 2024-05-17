using ProjectSteppe.Interactions.Interactables;
using ProjectSteppe.Managers;
using RicTools.Managers;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace ProjectSteppe.Managers
{
    public class LevelManager : GenericManager<LevelManager>
    {
        public CheckpointInteractable[] checkpoints;

        public int LevelIndex { get; private set; } = -1;

        protected override void Awake()
        {
            base.Awake();

            if (LevelIndex == -1 || LevelIndex != SceneManager.GetActiveScene().buildIndex)
            {
                LevelIndex = SceneManager.GetActiveScene().buildIndex;
            }

            for (int i = 0; i < checkpoints.Length; i++)
            {
                var checkpoint = checkpoints[i];
                checkpoint.levelIndex = i;
                checkpoint.OnCheckpointActivate.AddListener(SaveCheckpoint);

                if (!SaveManager.CurrentSave.level1Checkpoints.ContainsKey(checkpoint.id))
                {
                    SaveManager.CurrentSave.level1Checkpoints.Add(checkpoint.id, checkpoint.StartDiscovered);
                }

                if (!SaveManager.CurrentSave.level1Checkpoints[checkpoint.id] && checkpoint.StartDiscovered)
                {
                    SaveManager.CurrentSave.level1Checkpoints[checkpoint.id] = true;
                    checkpoint.StartDiscovered = SaveManager.CurrentSave.level1Checkpoints[checkpoint.id];
                    checkpoint.DiscoveredQuery();
                }
            }

            SaveManager.SaveGame();
        }

        private void SaveCheckpoint(CheckpointInteractable checkpoint)
        {
            SaveManager.CurrentSave.currentSceneIndex = LevelIndex;
            SaveManager.CurrentSave.currentCheckpointIndex = checkpoint.levelIndex;

            if (!SaveManager.CurrentSave.level1Checkpoints.ContainsKey(checkpoint.id))
                SaveManager.CurrentSave.level1Checkpoints.Add(checkpoint.id, checkpoint.StartDiscovered);

            SaveManager.SaveGame();
        }
    }
}
