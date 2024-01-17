using System;
using UnityEngine.UIElements;

namespace RicTools.Editor.EditorAttributes
{
    public class StringEditorVariableAttributeDrawer : EditorVariableAttributeDrawer
    {
        public override Type FieldType => typeof(string);

        public override VisualElement CreateVisualElement(EditorVariableDrawData editorVariableData)
        {
            var extraData = editorVariableData.extraData;

            TextField textField = new TextField();
            textField.label = editorVariableData.label;
            textField.value = (string)editorVariableData.value;

            if (!extraData.TryGetValue("multiline", out var multiline))
                multiline = false;

            textField.multiline = (bool)multiline;

            textField.RegisterValueChangedCallback(callback =>
            {
                editorVariableData.onValueChange?.Invoke(callback.newValue);
            });

            return textField;
        }
    }
}
