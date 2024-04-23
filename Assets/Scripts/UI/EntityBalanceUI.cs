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
            entityHealth.onBalanceChange.AddListener(OnBalanceChange);
        }

        private void OnBalanceChange(float balance, float maxBalance)
        {
            sliderL.value = balance / (float)maxBalance;
            sliderR.value = balance / (float)maxBalance;
        }
    }
}
