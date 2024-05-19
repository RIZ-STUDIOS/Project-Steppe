using ProjectSteppe.Managers;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace ProjectSteppe.Saving
{
    public static class SaveHandler
    {
        private static string SavePath => Application.persistentDataPath + "/Saves";

        public static SaveData CurrentSave { get; private set; }

        public static void SaveGame()
        {
            string save = JsonConvert.SerializeObject(CurrentSave);
            File.WriteAllText(SavePath + "/Save.json", save);
        }

        public static void LoadGame()
        {
            string readStr = File.ReadAllText(SavePath + "/Save.json");
            CurrentSave = JsonConvert.DeserializeObject<SaveData>(readStr);
        }

        public static bool InitSave()
        {
            if (File.Exists(SavePath + "/Save.json")) return false;

            CurrentSave = new SaveData();
            CurrentSave.currentSceneIndex = GameManager.Instance.defaultGameSceneIndex;

            Directory.CreateDirectory(SavePath);

            string save = JsonConvert.SerializeObject(CurrentSave);
            File.WriteAllText(SavePath + "/Save.json", save);

            return true;
        }

        public static void ResetSave()
        {
            File.Delete(SavePath + "/Save.json");
            InitSave();
        }
    }
}
