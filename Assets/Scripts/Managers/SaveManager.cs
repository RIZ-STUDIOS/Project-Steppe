using RicTools.Managers;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace ProjectSteppe.Managers
{
    public class SaveManager : GenericManager<SaveManager>
    {
        protected override bool DontDestroyManagerOnLoad => true;

        private string SavePath => Application.persistentDataPath + "/Saves";

        public SaveData CurrentSave { get; private set; }

        public void SaveGame()
        {
            string save = JsonUtility.ToJson(CurrentSave);
            File.WriteAllText(SavePath + "/Save.json", save);
        }

        public void LoadGame()
        {
            string readStr = File.ReadAllText(SavePath + "/Save.json");
            CurrentSave = JsonUtility.FromJson<SaveData>(readStr);
        }

        public bool InitSave()
        {
            if (File.Exists(SavePath + "/Save.json")) return false;

            CurrentSave = new SaveData();
            CurrentSave.currentSceneIndex = GameManager.Instance.defaultGameSceneIndex;

            Directory.CreateDirectory(SavePath);

            string save = JsonUtility.ToJson(CurrentSave);
            File.WriteAllText(SavePath + "/Save.json", save);

            return true;
        }
    }
}
