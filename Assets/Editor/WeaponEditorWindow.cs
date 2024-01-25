using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RicTools;
using RicTools.Editor.Windows;
using UnityEditor;

namespace ProjectSteppe.Editor
{
    public class WeaponEditorWindow : GenericEditorWindow<WeaponScriptableObject>
    {
        [MenuItem("Window/RicTools Windows/Weapon")]
    	public static WeaponEditorWindow ShowWindow()
        {
            return GetWindow<WeaponEditorWindow>("Weapon");
        }

        protected override void CreateEditorGUI()
        {
            CreateDefaultGUI();
        }

        protected override void LoadAsset(WeaponScriptableObject asset, bool isNull)
        {
        
        }

        protected override void SaveAsset(ref WeaponScriptableObject asset)
        {
        
        }
    }
}