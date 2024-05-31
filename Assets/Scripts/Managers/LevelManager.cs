using ProjectSteppe.Entities.Player;
using ProjectSteppe.Interactions.Interactables;
using ProjectSteppe.Saving;
using RicTools.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectSteppe.Managers
{
    public class LevelManager : GenericManager<LevelManager>
    {
        public CheckpointInteractable[] checkpoints;
        private InventoryInteractable[] items;

        public int LevelIndex { get; private set; } = -1;

        [SerializeField]
        private PlayerManager player;

        protected override void Awake()
        {
            base.Awake();

            items = GameObject.FindObjectsOfType<InventoryInteractable>();

            if (LevelIndex < 0 || LevelIndex != SceneManager.GetActiveScene().buildIndex)
            {
                LevelIndex = SceneManager.GetActiveScene().buildIndex;
            }

            InitItems();
            InitCheckpoints();

            if (
                SaveHandler.CurrentSave.currentCheckpointIndex >= checkpoints.Length ||
                SaveHandler.CurrentSave.currentCheckpointIndex < 0
               )
            {
                SaveHandler.CurrentSave.currentCheckpointIndex = 0;
            }

            SaveHandler.SaveGame();

            SetPlayerSpawnLocation();
        }

        private void SetPlayerSpawnLocation()
        {
            checkpoints[SaveHandler.CurrentSave.currentCheckpointIndex].spawnPoint.transform.GetPositionAndRotation(out var spawnPos, out var spawnRot);
            spawnRot.x = 0;
            spawnRot.z = 0;

            player.GetComponent<CharacterController>().enabled = false;

            player.transform.SetPositionAndRotation
                (
                    spawnPos,
                    spawnRot
                );

            player.GetComponent<CharacterController>().enabled = true;
        }

        private void InitCheckpoints()
        {
            for (int i = 0; i < checkpoints.Length; i++)
            {
                var checkpoint = checkpoints[i];
                checkpoint.levelIndex = i;
                checkpoint.OnCheckpointActivate.AddListener(SaveActivatedCheckpoint);

                if (!SaveHandler.CurrentSave.checkpointStates.ContainsKey(checkpoint.id))
                {
                    SaveHandler.CurrentSave.checkpointStates.Add(checkpoint.id, checkpoint.StartDiscovered);
                }

                if (!SaveHandler.CurrentSave.checkpointStates[checkpoint.id] && checkpoint.StartDiscovered)
                {
                    SaveHandler.CurrentSave.checkpointStates[checkpoint.id] = true;
                }

                checkpoint.StartDiscovered = SaveHandler.CurrentSave.checkpointStates[checkpoint.id];

                checkpoint.DiscoveredQuery();
            }
        }

        private void InitItems()
        {
            for (int i = 0; i < items.Length; i++)
            {
                var item = items[i];
                item.OnPickUp.AddListener(SavePickedUpInventory);

                if (!SaveHandler.CurrentSave.pickupItemStates.ContainsKey(item.itemSO.title))
                {
                    SaveHandler.CurrentSave.pickupItemStates.Add(item.itemSO.title, item.Interacted);
                }

                if (!SaveHandler.CurrentSave.pickupItemStates[item.itemSO.title] && item.Interacted)
                {
                    SaveHandler.CurrentSave.pickupItemStates[item.itemSO.title] = true;
                }

                item.Interacted = SaveHandler.CurrentSave.pickupItemStates[item.itemSO.title];
            }
        }

        private void SavePickedUpInventory(InventoryInteractable item)
        {
            SaveHandler.CurrentSave.pickupItemStates[item.itemSO.title] = item.Interacted;
            SaveHandler.CurrentSave.playerInventoryIDs.Add(item.itemSO.title);
            SaveHandler.SaveGame();
        }

        private void SaveActivatedCheckpoint(CheckpointInteractable checkpoint)
        {
            SaveHandler.CurrentSave.currentSceneIndex = LevelIndex;
            SaveHandler.CurrentSave.currentCheckpointIndex = checkpoint.levelIndex;
            SaveHandler.CurrentSave.checkpointStates[checkpoint.id] = true;

            SaveHandler.SaveGame();
        }
    }
}
