using RicTools.EditorAttributes;
using RicTools.ScriptableObjects;

namespace ProjectSteppe.ScriptableObjects
{
    [DefaultScriptableObjectName(nameof(weaponName))]
    public class WeaponScriptableObject : GenericScriptableObject
    {
        [EditorVariable] public string weaponName;
        [EditorVariable] public int damage;
        [EditorVariable] public int postureDamage;
    }
}