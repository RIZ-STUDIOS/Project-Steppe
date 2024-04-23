using UnityEngine;

namespace ProjectSteppe
{
    public class AIAnimator : MonoBehaviour
    {
        public Animator animController;

        public bool attackCommitted;
        public bool hyperArmour;

        private BossHandler bossHandler;

        private void Start()
        {
            bossHandler = GetComponent<BossHandler>();
        }

        public void RotateToTarget(AIController controller)
        {
            if (controller.isMoving)
            {
                animController.transform.rotation = controller.navmesh.transform.rotation;
            }
        }

        public void SetStates(AIController controller)
        {
            animController.SetBool("Chase", controller.isMoving);
            animController.SetBool("Idle", !controller.isMoving);
        }

        public void PlayTargetAnimation(string animationName)
        {
            int attackOption;
            int finisherOption;

            if (bossHandler.currentPhase > 0)
            {
                attackOption = Random.Range(0, 4);
                finisherOption = Random.Range(0, 5);
            }
            else
            {
                attackOption = Random.Range(0, 2);

                if (attackOption == 1) finisherOption = 0;
                else finisherOption = Random.Range(0, 2);
            }

            animController.SetInteger("AttackOption", attackOption);
            animController.SetInteger("FinisherOption", finisherOption);
            animController.SetBool("IsAttacking", true);
        }

        public void StoppedAttacking()
        {
            animController.SetBool("IsAttacking", false);
        }

        public void EnableAttackCommitted()
        {
            attackCommitted = true;
        }

        public void DisableAttackCommitted()
        {
            attackCommitted = false;
        }

        public void EnableHyperArmour()
        {
            hyperArmour = true;
        }

        public void DisableHyperArmour()
        {
            hyperArmour = false;
        }
    }
}
