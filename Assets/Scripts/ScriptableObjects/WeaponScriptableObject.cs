using RicTools.EditorAttributes;
using RicTools.ScriptableObjects;
using System;

namespace ProjectSteppe.ScriptableObjects
{
    [DefaultScriptableObjectName(nameof(weaponName))]
    [Obsolete]
    public class WeaponScriptableObject : GenericScriptableObject
    {
        [EditorVariable] public string weaponName;
        [EditorVariable] public int damage;
        [EditorVariable] public int postureDamage;
    }
}