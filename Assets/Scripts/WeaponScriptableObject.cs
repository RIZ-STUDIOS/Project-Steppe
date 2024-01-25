using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RicTools;
using RicTools.ScriptableObjects;
using RicTools.EditorAttributes;

public class WeaponScriptableObject : GenericScriptableObject
{
	[EditorVariable] public int damage;
	[EditorVariable] public int postureDamage;
}