using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RicTools;
using RicTools.Editor.Windows;
using UnityEditor;
using ProjectSteppe.ScriptableObjects;

namespace ProjectSteppe.Editor
{
    public class LoadingTipEditorWindow : GenericEditorWindow<LoadingTipScriptableObject>
    {
        [MenuItem("Window/RicTools Windows/Loading Tip Editor")]
    	public static LoadingTipEditorWindow ShowWindow()
        {
            return GetWindow<LoadingTipEditorWindow>("Loading Tip Editor");
        }

        protected override void CreateEditorGUI()
        {
            CreateDefaultGUI();
        }

        protected override void LoadAsset(LoadingTipScriptableObject asset, bool isNull)
        {
        
        }

        protected override void SaveAsset(ref LoadingTipScriptableObject asset)
        {
        
        }
    }
}