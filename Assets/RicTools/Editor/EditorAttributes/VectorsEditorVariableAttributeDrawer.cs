using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace RicTools.Editor.EditorAttributes
{
    public class Vector2IntEditorVariableAttributeDrawer : EditorVariableAttributeDrawer
    {
        public override Type FieldType => typeof(Vector2Int);

        public override VisualElement CreateVisualElement(EditorVariableDrawData editorVariableData)
        {
            var field = new Vector2IntField();
            field.label = editorVariableData.label;
            field.value = (Vector2Int)editorVariableData.value;

            field.RegisterValueChangedCallback(callback =>
            {
                editorVariableData.onValueChange?.Invoke(callback.newValue);
            });

            return field;
        }
    }

    public class Vector3IntEditorVariableAttributeDrawer : EditorVariableAttributeDrawer
    {
        public override Type FieldType => typeof(Vector3Int);

        public override VisualElement CreateVisualElement(EditorVariableDrawData editorVariableData)
        {
            var field = new Vector3IntField();
            field.label = editorVariableData.label;
            field.value = (Vector3Int)editorVariableData.value;

            field.RegisterValueChangedCallback(callback =>
            {
                editorVariableData.onValueChange?.Invoke(callback.newValue);
            });

            return field;
        }
    }

    public class Vector2EditorVariableAttributeDrawer : EditorVariableAttributeDrawer
    {
        public override Type FieldType => typeof(Vector2);

        public override VisualElement CreateVisualElement(EditorVariableDrawData editorVariableData)
        {
            var field = new Vector2Field();
            field.label = editorVariableData.label;
            field.value = (Vector2)editorVariableData.value;

            field.RegisterValueChangedCallback(callback =>
            {
                editorVariableData.onValueChange?.Invoke(callback.newValue);
            });

            return field;
        }
    }

    public class Vector3EditorVariableAttributeDrawer : EditorVariableAttributeDrawer
    {
        public override Type FieldType => typeof(Vector3);

        public override VisualElement CreateVisualElement(EditorVariableDrawData editorVariableData)
        {
            var field = new Vector3Field();
            field.label = editorVariableData.label;
            field.value = (Vector3)editorVariableData.value;

            field.RegisterValueChangedCallback(callback =>
            {
                editorVariableData.onValueChange?.Invoke(callback.newValue);
            });

            return field;
        }
    }
}
