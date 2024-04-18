using ProjectSteppe.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.Entities.Player
{
    public class PlayerInventory : MonoBehaviour
    {
        public List<InventoryItemScriptableObject> items = new List<InventoryItemScriptableObject>();
    }
}
