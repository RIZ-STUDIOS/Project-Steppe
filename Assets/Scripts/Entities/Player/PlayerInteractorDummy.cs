using ProjectSteppe.AI;
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
            if (Time.timeScale == 0) return;
            if (GetComponent<EntityHealth>().Health <= 0) return;
            if (GetComponent<AITarget>().nearbyControllers.Count > 0) return;
            interactor.OnInteract();
        }
    }
}
