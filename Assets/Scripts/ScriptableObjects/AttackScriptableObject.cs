using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RicTools;
using RicTools.ScriptableObjects;
using RicTools.EditorAttributes;
using UnityEngine.Serialization;

namespace ProjectSteppe.ScriptableObjects
{
    [DefaultScriptableObjectName(nameof(attackName))]
    public class AttackScriptableObject : GenericScriptableObject
    {
        [EditorVariable]
        public string attackName;

        [EditorVariable]
        [FormerlySerializedAs("healthAttack")]
        public float healthDamage;

        [EditorVariable]
        public float balanceDamage;

        [SliderEditorVariable(0, 1)]
        public float healthBlockPassthrough;

        [SliderEditorVariable(0, 1)]
        public float balanceBlockPassthrough;
    }
}