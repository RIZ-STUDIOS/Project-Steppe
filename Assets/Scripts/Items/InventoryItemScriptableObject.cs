using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.Items
{
    [CreateAssetMenu(fileName = "InventoryItem", menuName = "1543493/InventoryItem")]
    public class InventoryItemScriptableObject : ScriptableObject
    {
        public Sprite icon;
        public InvetoryItemType itemType;
        public string title;

        [TextArea(3,6)]
        public string description;
    }

    public enum InvetoryItemType
    {
        None,
        Weapon,
        Armour,
        Consumable,
        Misc
    }
}
