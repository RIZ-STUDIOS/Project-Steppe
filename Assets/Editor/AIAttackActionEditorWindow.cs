using RicTools.Editor.Windows;
using UnityEditor;

namespace ProjectSteppe.Editor
{
    public class AIAttackActionEditorWindow : GenericEditorWindow<AIAttackActionScriptableObject>
    {
        [MenuItem("Window/RicTools Windows/AIAttackAction")]
        public static AIAttackActionEditorWindow ShowWindow()
        {
            return GetWindow<AIAttackActionEditorWindow>("AIAttackAction");
        }

        protected override void CreateEditorGUI()
        {
            CreateDefaultGUI();
        }

        protected override void LoadAsset(AIAttackActionScriptableObject asset, bool isNull)
        {

        }

        protected override void SaveAsset(ref AIAttackActionScriptableObject asset)
        {

        }
    }
}