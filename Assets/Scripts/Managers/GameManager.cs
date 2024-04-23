using RicTools.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace ProjectSteppe.Managers
{
    public class GameManager : GenericManager<GameManager>
    {
        public bool hasSecondCheckpoint;

        public void RespawnCharacter()
        {
            if (!hasSecondCheckpoint) LoadingManager.LoadScene(1);
            else LoadingManager.LoadScene(3);
        }

        public void HasSecondCheckpoint()
        {
            hasSecondCheckpoint = true;
        }
    }
}
