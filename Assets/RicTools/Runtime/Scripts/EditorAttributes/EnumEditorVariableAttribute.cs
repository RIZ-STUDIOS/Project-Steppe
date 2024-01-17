using RicTools.Utilities;

namespace RicTools.EditorAttributes
{
    public class EnumEditorVariableAttribute : EditorVariableAttribute
    {
        public bool UseFlagField
        {
            get
            {
                if (!ExtraData.TryGetValue("isFlagField", out var isFlagField))
                    isFlagField = false;
                return (bool)isFlagField;
            }
            set
            {
                ExtraData.Set("isFlagField", value);
            }
        }

        public EnumEditorVariableAttribute()
        {
            UseFlagField = false;
        }
    }
}
