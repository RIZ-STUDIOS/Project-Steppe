using RicTools.Utilities;

namespace RicTools.EditorAttributes
{
    public class ColorEditorVariableAttribute : EditorVariableAttribute
    {
        public bool ShowAlpha
        {
            get
            {
                if (!ExtraData.TryGetValue("showAlpha", out var value))
                    value = true;
                return (bool)value;
            }
            set
            {
                ExtraData.Set("showAlpha", value);
            }
        }

        public bool HDR
        {
            get
            {
                if (!ExtraData.TryGetValue("hdr", out var value))
                    value = false;
                return (bool)value;
            }
            set
            {
                ExtraData.Set("hdr", value);
            }
        }
    }
}
