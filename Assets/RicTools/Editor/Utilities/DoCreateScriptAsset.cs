using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

namespace RicTools.Editor.Utilities
{
    public class DoCreateScriptAsset : EndNameEditAction
    {
        public override void Action(int instanceId, string pathName, string resourceFile)
        {
            Object o = FileUtilities.CreateScriptAssetFromTemplate(pathName, resourceFile, CustomReplaces);
            ProjectWindowUtil.ShowCreatedAsset(o);
        }

        protected virtual string CustomReplaces(string content)
        {
            return content;
        }
    }
}
