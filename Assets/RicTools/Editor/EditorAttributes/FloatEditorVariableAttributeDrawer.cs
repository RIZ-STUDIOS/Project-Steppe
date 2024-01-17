using System;
using UnityEngine.UIElements;

namespace RicTools.Editor.EditorAttributes
{
    public class FloatEditorVariableAttributeDrawer : EditorVariableAttributeDrawer
    {
        public override Type FieldType => typeof(float);

        public override VisualElement CreateVisualElement(EditorVariableDrawData editorVariableData)
        {
            var value = editorVariableData.value;
            var extraData = editorVariableData.extraData;

            if (value.GetType() == typeof(int))
                value = Convert.ToSingle(value);

            if (extraData.TryGetValue("isSlider", out var _))
            {
                extraData.TryGetValue("minValue", out var minValue);
                extraData.TryGetValue("maxValue", out var maxValue);

                if (minValue.GetType() == typeof(int))
                    minValue = Convert.ToSingle(minValue);

                if (maxValue.GetType() == typeof(int))
                    maxValue = Convert.ToSingle(maxValue);

                if (!extraData.TryGetValue("showInputField", out var showInputField))
                    showInputField = true;

                var slider = new Slider()
                {
                    lowValue = (float)minValue,
                    highValue = (float)maxValue,
                    showInputField = (bool)showInputField,
                    label = editorVariableData.label,
                    value = (float)value
                };

                slider.RegisterValueChangedCallback(callback =>
                {
                    editorVariableData.onValueChange?.Invoke(callback.newValue);
                });

                return slider;
            }

            var floatField = new FloatField();
            floatField.label = editorVariableData.label;
            floatField.value = (float)value;

            floatField.RegisterValueChangedCallback(callback =>
            {
                editorVariableData.onValueChange?.Invoke(callback.newValue);
            });

            return floatField;
        }
    }
}
