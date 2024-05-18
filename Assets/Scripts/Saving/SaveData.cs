using System.Collections.Generic;

namespace ProjectSteppe.Saving
{
    public class SaveData
    {
        public int currentSceneIndex;
        public int currentCheckpointIndex;
        public Dictionary<string, bool> checkpointStates = new();
        public Dictionary<string, bool> itemStates = new();
    }
}
