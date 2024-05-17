using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe
{
    public class SaveData
    {
        public int currentSceneIndex;
        public int currentCheckpointIndex;
        public Dictionary<string,bool> level1Checkpoints = new();
    }
}
