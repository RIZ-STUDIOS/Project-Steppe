using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.AI.States
{
    //[CreateAssetMenu(menuName = "AI/States/Combo Attack")]
    public class ComboAIAttackState : AIAttackState
    {
        [SerializeField]
        private ComboData[] attacks;

        private Coroutine comboCoroutine;

        private AIAttackState attack;

        public override bool CanUseAttack()
        {
            return true;
        }

        public override void Execute()
        {
            comboCoroutine = controller.StartCoroutine(ComboCoroutine());
        }

        private IEnumerator ComboCoroutine()
        {
            for (int i = 0; i < attacks.Length; i++)
            {
                attack = GameObject.Instantiate(attacks[i].attack);
                if (!attack.attackScriptableObject)
                    attack.attackScriptableObject = attackHandler.defaultAttackScriptableObject;
                attack.controller = controller;
                attack.attackHandler = attackHandler;
                attack.Execute();
                float timer = 0;
                while (!attack.attackFinished)
                {
                    timer += Time.deltaTime;
                    if (attacks[i].useCutOffTime && timer >= attacks[i].cutOffTime)
                    {
                        attack.OnForceExit();
                        attack.FinishAttack();
                    }
                    yield return null;
                }
            }
            attack = null;
            attackFinished = true;
        }

        public override void OnForceExit()
        {
            if (comboCoroutine != null)
            {
                controller.StopCoroutine(comboCoroutine);
                if (attack != null)
                    attack.OnForceExit();
            }
            else
            {
                base.OnForceExit();
            }
        }

        [System.Serializable]
        private struct ComboData
        {
            public AIAttackState attack;
            public bool useCutOffTime;
            public float cutOffTime;
        }
    }
}
