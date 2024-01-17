namespace RicTools.Editor.Utilities
{
    internal class DoCreateEditorAsset : DoCreateScriptAsset
    {
        internal string scriptableObject;

        protected override string CustomReplaces(string content)
        {
            content = content.Replace("#SCRIPTABLEOBJECT#", scriptableObject);
            return content;
        }
    }
}
