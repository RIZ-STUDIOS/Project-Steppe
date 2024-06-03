using RicTools.EditorAttributes;
using RicTools.ScriptableObjects;
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

        [EditorVariable]
        public float perfectBlockBalanceDamage;

        [EditorVariable]
        public bool balanceBlockPassthrough;
    }
}