using System;
using UnityEngine.UIElements;

namespace RicTools.Editor.EditorAttributes
{
    public class BoolEditorVariableAttributeDrawer : EditorVariableAttributeDrawer
    {
        public override Type FieldType => typeof(bool);

        public override VisualElement CreateVisualElement(EditorVariableDrawData editorVariableData)
        {
            var toggle = new Toggle();
            toggle.label = editorVariableData.label;
            toggle.value = (bool)editorVariableData.value;

            toggle.RegisterValueChangedCallback(callback =>
            {
                editorVariableData.onValueChange?.Invoke(callback.newValue);
            });

            return toggle;
        }
    }
}
