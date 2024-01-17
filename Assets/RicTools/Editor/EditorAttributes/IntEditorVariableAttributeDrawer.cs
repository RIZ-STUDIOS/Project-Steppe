using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace RicTools.Editor.EditorAttributes
{
    public class IntEditorVariableAttributeDrawer : EditorVariableAttributeDrawer
    {
        public override Type FieldType => typeof(int);

        public override VisualElement CreateVisualElement(EditorVariableDrawData editorVariableData)
        {
            var extraData = editorVariableData.extraData;

            if (extraData.TryGetValue("isSlider", out var _))
            {
                extraData.TryGetValue("minValue", out var minValue);
                if (minValue.GetType() == typeof(float))
                    minValue = (int)Mathf.Floor((float)minValue);

                extraData.TryGetValue("maxValue", out var maxValue);
                if (maxValue.GetType() == typeof(float))
                    maxValue = (int)Mathf.Floor((float)maxValue);

                if (!extraData.TryGetValue("showInputField", out var showInputField))
                    showInputField = true;

                var sliderInt = new SliderInt()
                {
                    lowValue = (int)minValue,
                    highValue = (int)maxValue,
                    showInputField = (bool)showInputField,
                    label = editorVariableData.label,
                    value = (int)editorVariableData.value
                };

                sliderInt.RegisterValueChangedCallback(callback =>
                {
                    editorVariableData.onValueChange?.Invoke(callback.newValue);
                });

                return sliderInt;
            }

            IntegerField integerField = new IntegerField();
            integerField.label = editorVariableData.label;
            integerField.value = (int)editorVariableData.value;

            integerField.RegisterValueChangedCallback(callback =>
            {
                editorVariableData.onValueChange?.Invoke(callback.newValue);
            });

            return integerField;
        }
    }
}
