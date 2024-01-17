using System;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace RicTools.Editor.EditorAttributes
{
    public class ObjectEditorVariableAttributeDrawer : EditorVariableAttributeDrawer
    {
        public override Type FieldType => typeof(UnityEngine.Object);

        public override VisualElement CreateVisualElement(EditorVariableDrawData editorVariableData)
        {
            var extraData = editorVariableData.extraData;

            var objectField = new ObjectField();

            if (!extraData.TryGetValue("allowSceneObjects", out var allowSceneObjects))
            {
                allowSceneObjects = false;
            }

            objectField.label = editorVariableData.label;
            objectField.allowSceneObjects = (bool)allowSceneObjects;
            objectField.value = (UnityEngine.Object)editorVariableData.value;
            objectField.objectType = editorVariableData.fieldType;

            objectField.RegisterValueChangedCallback(callback =>
            {
                editorVariableData.onValueChange?.Invoke(callback.newValue);
            });

            return objectField;
        }
    }
}
