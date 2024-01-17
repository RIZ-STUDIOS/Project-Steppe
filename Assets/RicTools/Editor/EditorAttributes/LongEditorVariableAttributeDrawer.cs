using System;
using UnityEngine.UIElements;

namespace RicTools.Editor.EditorAttributes
{
    public class LongEditorVariableAttributeDrawer : EditorVariableAttributeDrawer
    {
        public override Type FieldType => typeof(long);

        public override VisualElement CreateVisualElement(EditorVariableDrawData editorVariableData)
        {
            var longField = new LongField();

            longField.label = editorVariableData.label;
            longField.value = (long)editorVariableData.value;

            longField.RegisterValueChangedCallback(callback =>
            {
                editorVariableData.onValueChange?.Invoke(callback.newValue);
            });

            return longField;
        }
    }
}
