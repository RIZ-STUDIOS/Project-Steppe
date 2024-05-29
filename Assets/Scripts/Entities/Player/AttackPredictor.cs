using ProjectSteppe.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.Entities.Player
{
    public class AttackPredictor : MonoBehaviour
    {
        private PlayerAttackController attackController;

        private List<NewAIController> enemies = new List<NewAIController>();

        private void Awake()
        {
            attackController = GetComponentInParent<PlayerAttackController>();

            attackController.onAttack.AddListener(OnAttack);
        }

        private void OnTriggerEnter(Collider other)
        {
            var controller = other.GetComponent<NewAIController>();
            if (controller && !enemies.Contains(controller))
            {
                Debug.Log(other.name + " entered attack predictor");
                enemies.Add(controller);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var controller = other.GetComponent<NewAIController>();
            if (controller && enemies.Contains(controller))
            {
                Debug.Log(other.name + " exit attack predictor");
                enemies.Remove(controller);
            }
        }

        private void OnAttack()
        {
            foreach(var controller in enemies)
            {
                controller.playerAttacking = true;
            }
        }
    }
}
