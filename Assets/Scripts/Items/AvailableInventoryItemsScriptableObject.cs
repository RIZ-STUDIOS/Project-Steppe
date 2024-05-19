using ProjectSteppe.Items;
using UnityEngine;

namespace ProjectSteppe
{
    [CreateAssetMenu(fileName = "AvailableInventoryItems", menuName = "1543493/AvailableInventoryItems")]
    public class AvailableInventoryItemsScriptableObject : ScriptableObject
    {
        public InventoryItemScriptableObject[] items;
    }
}
