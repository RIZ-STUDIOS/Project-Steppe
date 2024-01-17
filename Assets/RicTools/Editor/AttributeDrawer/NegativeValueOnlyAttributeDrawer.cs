using RicTools.Attributes;
using RicTools.Utilities;
using UnityEditor;
using UnityEngine;

namespace RicTools.Editor.AttributeDrawer
{
    [CustomPropertyDrawer(typeof(NegativeValueOnlyAttribute))]
    public class NegativeValueOnlyAttributeDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!property.IsNumerical() && !property.IsVector())
            {
                EditorGUI.HelpBox(position, "[NegativeValueOnly] used with non-numeric property", MessageType.Warning);
                return;
            }
            if (property.HandleMaxValues(0)) property.serializedObject.ApplyModifiedProperties();
            EditorGUI.PropertyField(position, property, label, true);
        }
    }
}
