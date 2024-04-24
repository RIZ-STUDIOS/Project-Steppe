using ProjectSteppe.Entities.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.Entities.Player
{
    public class PlayerInteractorDummy : MonoBehaviour
    {
        private PlayerInteractor interactor;

        private void Awake()
        {
            interactor = GetComponentInChildren<PlayerInteractor>();
        }

        private void OnInteract()
        {
            interactor.OnInteract();
        }
    }
}
