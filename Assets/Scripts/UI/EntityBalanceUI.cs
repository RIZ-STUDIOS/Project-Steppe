using ProjectSteppe.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectSteppe.UI
{
    public class EntityBalanceUI : MonoBehaviour
    {
        [SerializeField]
        private EntityHealth entityHealth;

        [SerializeField]
        private Slider sliderL;

        [SerializeField]
        private Slider sliderR;

        private void Start()
        {
            entityHealth.onPostureChange.AddListener(OnBalanceChange);
        }

        private void OnBalanceChange(int balance, int maxBalance)
        {
            sliderL.value = balance;
            sliderR.value = balance;
        }
    }
}
