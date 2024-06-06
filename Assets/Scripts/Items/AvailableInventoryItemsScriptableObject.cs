using ProjectSteppe.Items;
using UnityEngine;

namespace ProjectSteppe
{
    [CreateAssetMenu(fileName = "AvailableInventoryItems", menuName = "Steppe/Items/AvailableInventoryItems")]
    public class AvailableInventoryItemsScriptableObject : ScriptableObject
    {
        public InventoryItemScriptableObject[] items;
    }
}
