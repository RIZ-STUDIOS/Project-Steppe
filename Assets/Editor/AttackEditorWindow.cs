using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RicTools;
using RicTools.Editor.Windows;
using UnityEditor;
using ProjectSteppe.ScriptableObjects;

namespace ProjectSteppe.Editor
{
    public class AttackEditorWindow : GenericEditorWindow<AttackScriptableObject>
    {
        [MenuItem("Window/RicTools Windows/Attack Editor")]
    	public static AttackEditorWindow ShowWindow()
        {
            return GetWindow<AttackEditorWindow>("Attack Editor");
        }

        protected override void CreateEditorGUI()
        {
            CreateDefaultGUI();
        }

        protected override void LoadAsset(AttackScriptableObject asset, bool isNull)
        {
        
        }

        protected override void SaveAsset(ref AttackScriptableObject asset)
        {
        
        }
    }
}