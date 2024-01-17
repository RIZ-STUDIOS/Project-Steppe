using RicTools.Utilities;

namespace RicTools.EditorAttributes
{
    public class ObjectEditorVariableAttribute : EditorVariableAttribute
    {
        public bool AllowSceneObjects
        {
            get
            {
                if (!ExtraData.TryGetValue("allowSceneObjects", out var data))
                    data = false;
                return (bool)data;
            }
            set { ExtraData.Set("allowSceneObjects", value); }
        }
    }
}
