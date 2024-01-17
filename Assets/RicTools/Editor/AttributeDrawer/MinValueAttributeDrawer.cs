using RicTools.Attributes;
using RicTools.Utilities;
using UnityEditor;
using UnityEngine;

namespace RicTools.Editor
{
    [CustomPropertyDrawer(typeof(MinValueAttribute))]
    public class MinValueAttributeDrawer : PropertyDrawer
    {
        private MinValueAttribute cachedAttribute;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (cachedAttribute == null) cachedAttribute = attribute as MinValueAttribute;

            if (!property.IsNumerical() && !property.IsVector())
            {
                EditorGUI.HelpBox(position, "[MinValue] used with non-numeric property", MessageType.Warning);
                return;
            }
            if ((property.IsNumerical() && property.HandleMinNumericalValues(cachedAttribute.X)) ||
                (property.IsVectorInt() && property.HandleMinVectorIntValues(new Vector3Int((int)cachedAttribute.X, (int)cachedAttribute.Y, (int)cachedAttribute.Z))) ||
                (property.IsVectorFloat() && property.HandleMinVectorValues(new Vector4(cachedAttribute.X, cachedAttribute.Y, cachedAttribute.Z, cachedAttribute.W))))
                property.serializedObject.ApplyModifiedProperties();
            EditorGUI.PropertyField(position, property, label, true);
        }
    }
}
