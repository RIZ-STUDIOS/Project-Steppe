using System;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace RicTools.Editor.EditorAttributes
{
    public class EnumEditorVariableAttributeDrawer : EditorVariableAttributeDrawer
    {
        public override Type FieldType => typeof(Enum);

        public override VisualElement CreateVisualElement(EditorVariableDrawData editorVariableData)
        {
            var extraData = editorVariableData.extraData;

            if (!extraData.TryGetValue("isFlagField", out var isFlagField))
                isFlagField = false;
            if ((bool)isFlagField)
            {
                var enumFlagField = new EnumFlagsField()
                {
                    label = editorVariableData.label,
                    value = (Enum)editorVariableData.value
                };
                enumFlagField.Init((Enum)editorVariableData.value);

                enumFlagField.RegisterValueChangedCallback(callback =>
                {
                    editorVariableData.onValueChange?.Invoke(callback.newValue);
                });

                return enumFlagField;
            }

            var enumField = new EnumField()
            {
                label = editorVariableData.label,
                value = (Enum)editorVariableData.value
            };
            enumField.Init((Enum)editorVariableData.value);

            enumField.RegisterValueChangedCallback(callback =>
            {
                editorVariableData.onValueChange?.Invoke(callback.newValue);
            });

            return enumField;
        }
    }
}
