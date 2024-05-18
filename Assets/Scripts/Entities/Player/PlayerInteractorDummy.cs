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
