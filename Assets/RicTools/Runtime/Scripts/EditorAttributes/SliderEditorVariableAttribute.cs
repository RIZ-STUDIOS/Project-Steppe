using RicTools.Utilities;

namespace RicTools.EditorAttributes
{
    public class SliderEditorVariableAttribute : EditorVariableAttribute
    {
        public bool ShowInputField
        {
            get
            {
                if (!ExtraData.TryGetValue("showInputField", out var data))
                    return false;
                return (bool)data;
            }
            set
            {
                ExtraData.Set("showInputField", value);
            }
        }

        public SliderEditorVariableAttribute(int minValue, int maxValue)
        {
            ExtraData.Add("isSlider", true);
            ExtraData.Add("minValue", minValue);
            ExtraData.Add("maxValue", maxValue);
            ShowInputField = true;
        }

        public SliderEditorVariableAttribute(float minValue, float maxValue)
        {
            ExtraData.Add("isSlider", true);
            ExtraData.Add("minValue", minValue);
            ExtraData.Add("maxValue", maxValue);
            ShowInputField = true;
        }
    }
}
