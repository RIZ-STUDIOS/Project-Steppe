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

        [SerializeField]
        private int healthBarIndex;

        private void Start()
        {
            entityHealth.onHealthChange.AddListener(OnHealthChange);
        }

        private void OnHealthChange(float health, float maxHealth)
        {
            if (healthBarIndex == entityHealth.healthBarIndex)
                slider.value = health / (float)maxHealth;
        }
    }
}
