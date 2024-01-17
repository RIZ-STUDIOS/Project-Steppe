using RicTools.Attributes;
using UnityEditor;
using UnityEngine;

namespace RicTools.Editor.AttributeDrawer
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyAttributeDrawer : PropertyDrawer
    {
        private ReadOnlyAttribute cachedAttribute;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (cachedAttribute == null) cachedAttribute = attribute as ReadOnlyAttribute;

            var enabled = cachedAttribute.PlayMode == AvailableMode.All
                || ((EditorApplication.isPlaying && cachedAttribute.PlayMode == AvailableMode.Play)
                || (!EditorApplication.isPlaying && cachedAttribute.PlayMode == AvailableMode.Editor));

            GUI.enabled = !enabled;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
}
