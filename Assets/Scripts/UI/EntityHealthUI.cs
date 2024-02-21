using ProjectSteppe.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectSteppe.UI
{
    public class EntityHealthUI : MonoBehaviour
    {
        [SerializeField]
        private EntityHealth entityHealth;

        [SerializeField]
        private Slider slider;

        private void Start()
        {
            entityHealth.onHealthChange.AddListener(OnHealthChange);
        }

        private void OnHealthChange(float health, float maxHealth)
        {
            slider.value = health/(float)maxHealth;
        }
    }
}
