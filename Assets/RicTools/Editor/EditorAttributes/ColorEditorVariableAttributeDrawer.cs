using System;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace RicTools.Editor.EditorAttributes
{
    public class ColorEditorVariableAttributeDrawer : EditorVariableAttributeDrawer
    {
        public override Type FieldType => typeof(Color);

        public override VisualElement CreateVisualElement(EditorVariableDrawData editorVariableData)
        {
            var colorField = new ColorField();

            colorField.label = editorVariableData.label;

            colorField.value = (Color)editorVariableData.value;

            var extraData = editorVariableData.extraData;

            if (!extraData.TryGetValue("showAlpha", out var showAlpha))
            {
                showAlpha = true;
            }
            if (!extraData.TryGetValue("hdr", out var hdr))
            {
                hdr = false;
            }

            colorField.showAlpha = (bool)showAlpha;
            colorField.hdr = (bool)hdr;

            colorField.RegisterValueChangedCallback(callback =>
            {
                editorVariableData.onValueChange?.Invoke(callback.newValue);
            });

            return colorField;
        }
    }
}
