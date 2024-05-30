using RicTools.EditorAttributes;
using RicTools.ScriptableObjects;
using UnityEngine;


namespace ProjectSteppe
{
    [CreateAssetMenu(fileName = "AIAttackSO", menuName = "1543493/AIAttackSO")]
    public class AIAttackActionScriptableObject : ScriptableObject
    {
        public string attackAnimName;

        public AIAttackActionScriptableObject comboAction;
        public bool canThisAnimationCombo = false;

        public float weight = 50f;
        public float attackRecovery = 1.5f;
        public float minDistanceToTarget = 0f;
        public float maxDistanceToTarget = 5f;
    }
}