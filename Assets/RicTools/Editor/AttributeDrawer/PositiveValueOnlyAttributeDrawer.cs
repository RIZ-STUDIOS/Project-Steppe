using RicTools.Attributes;
using RicTools.Utilities;
using UnityEditor;
using UnityEngine;

namespace RicTools.Editor.AttributeDrawer
{
    [CustomPropertyDrawer(typeof(PositiveValueOnlyAttribute))]
    public class PositiveValueOnlyAttributeDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!property.IsNumerical() && !property.IsVector())
            {
                EditorGUI.HelpBox(position, "[PositiveValueOnly] used with non-numeric property", MessageType.Warning);
                return;
            }
            if (property.HandleMinValues(0)) property.serializedObject.ApplyModifiedProperties();
            EditorGUI.PropertyField(position, property, label, true);
        }
    }
}
