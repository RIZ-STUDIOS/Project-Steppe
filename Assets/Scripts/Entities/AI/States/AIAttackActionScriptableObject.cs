using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RicTools;
using RicTools.ScriptableObjects;
using RicTools.EditorAttributes;

namespace ProjectSteppe
{
    [DefaultScriptableObjectName(nameof(nameOfAttackSO))]
    public class AIAttackActionScriptableObject : GenericScriptableObject
    {
        [EditorVariable] public string nameOfAttackSO;

        [EditorVariable] public string attackAnimation;

        [EditorVariable] public AIAttackActionScriptableObject comboAction;
        [EditorVariable] public bool canThisAnimationCombo = false;

        [EditorVariable] public float weight = 50f;
        [EditorVariable] public float attackRecovery = 1.5f;
        [EditorVariable] public float minDistanceToTarget = 0f;
        [EditorVariable] public float maxDistanceToTarget = 5f;
    }
}