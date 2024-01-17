using RicTools.EditorAttributes;
using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace RicTools.Editor.EditorAttributes
{
    public abstract class EditorVariableAttributeDrawer
    {
        public Type EditorVariableAttributeType { get; private set; }
        public abstract Type FieldType { get; }

        public EditorVariableAttributeDrawer()
        {
            SetEditorVariableAttributeType<EditorVariableAttribute>();
        }

        public void SetEditorVariableAttributeType<T>() where T : EditorVariableAttribute
        {
            EditorVariableAttributeType = typeof(T);
        }

        public abstract VisualElement CreateVisualElement(EditorVariableDrawData data);

        public virtual object SaveData(VisualElement visualElement, Type fieldType)
        {
            var ValueProperty = visualElement.GetType().GetProperty("value");
            var value = ValueProperty.GetValue(visualElement);
            return value;
        }

        public virtual void LoadData(VisualElement visualElement, object data)
        {
            var valueProperty = visualElement.GetType().GetProperty("value");
            valueProperty.SetValue(visualElement, data);
        }
    }

    public sealed class EditorVariableDrawData
    {
        public readonly string label;
        public readonly object value;
        public readonly Type fieldType;
        public readonly Dictionary<string, object> extraData;
        public readonly Type innerType;
        public Action<object> onValueChange;

        public EditorVariableDrawData(string label, object value, Type fieldType, Dictionary<string, object> extraData, Type innerType)
        {
            this.label = label;
            this.value = value;
            this.fieldType = fieldType;
            this.extraData = extraData;
            this.innerType = innerType;
        }
    }
}
