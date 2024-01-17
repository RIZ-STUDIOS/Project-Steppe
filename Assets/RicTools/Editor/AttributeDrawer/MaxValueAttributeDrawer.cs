using RicTools.Attributes;
using RicTools.Utilities;
using UnityEditor;
using UnityEngine;

namespace RicTools.Editor.AttributeDrawer
{
    [CustomPropertyDrawer(typeof(MaxValueAttribute))]
    public class MaxValueAttributeDrawer : PropertyDrawer
    {
        private MaxValueAttribute cachedAttribute;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (cachedAttribute == null) cachedAttribute = attribute as MaxValueAttribute;

            if (!property.IsNumerical() && !property.IsVector())
            {
                EditorGUI.HelpBox(position, "[MaxValue] used with non-numeric property", MessageType.Warning);
                return;
            }
            if ((property.IsNumerical() && property.HandleMaxNumericalValues(cachedAttribute.X)) ||
                (property.IsVectorInt() && property.HandleMaxVectorIntValues(new Vector3Int((int)cachedAttribute.X, (int)cachedAttribute.Y, (int)cachedAttribute.Z))) ||
                (property.IsVectorFloat() && property.HandleMaxVectorValues(new Vector4(cachedAttribute.X, cachedAttribute.Y, cachedAttribute.Z, cachedAttribute.W))))
                property.serializedObject.ApplyModifiedProperties();
            EditorGUI.PropertyField(position, property, label, true);
        }
    }
}
