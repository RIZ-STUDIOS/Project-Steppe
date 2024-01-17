using RicTools.Utilities;

namespace RicTools.EditorAttributes
{
    public class TextBoxEditorVariableAttribute : EditorVariableAttribute
    {
        public TextBoxEditorVariableAttribute()
        {
            ExtraData.Set("multiline", true);
        }
    }
}
