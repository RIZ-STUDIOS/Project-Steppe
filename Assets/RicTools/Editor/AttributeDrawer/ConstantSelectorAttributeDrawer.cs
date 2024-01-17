using RicTools.Attributes;
using RicTools.Editor.Utilities;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RicTools.Editor.AttributeDrawer
{
    [CustomPropertyDrawer(typeof(ConstantSelectorAttribute))]
    public class ConstantSelectorAttributeDrawer : PropertyDrawer
    {
        private ConstantSelectorAttribute cachedProperty;
        private Type targetType;
        private string[] names;
        private object[] values;
        private int selectedIndex;
        private bool foundValue;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            CacheProperty(property);

            if (values == null || values.Length == 0)
            {
                EditorGUI.PropertyField(position, property, label);
                return;
            }

            if (targetType != fieldInfo.FieldType)
            {
                EditorGUI.HelpBox(position, $"[ConstantSelector] '{targetType}' is not the same as '{fieldInfo.FieldType}'", MessageType.Error);
                return;
            }

            if (!foundValue && selectedIndex == 0)
            {
                EditorGUIHelper.DrawBox(position, Color.yellow);
            }

            EditorGUI.BeginChangeCheck();
            selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, names);
            if (EditorGUI.EndChangeCheck())
            {
                fieldInfo.SetValue(property.serializedObject.targetObject, values[selectedIndex]);
                property.serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(property.serializedObject.targetObject);
            }
        }

        private void CacheProperty(SerializedProperty property)
        {
            if (cachedProperty == null)
            {
                cachedProperty = attribute as ConstantSelectorAttribute;

                targetType = fieldInfo.FieldType;

                var fields = targetType.GetFields(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.FlattenHierarchy);
                var properties = targetType.GetProperties(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.FlattenHierarchy);

                List<string> names = new List<string>();
                List<object> values = new List<object>();

                foreach (var field in fields)
                {
                    if ((field.IsInitOnly || field.IsLiteral) && field.FieldType == targetType)
                    {
                        names.Add(field.Name);
                        values.Add(field.GetValue(null));
                    }
                }

                foreach (var p in properties)
                {
                    if (p.PropertyType == targetType)
                    {
                        names.Add(p.Name);
                        values.Add(p.GetValue(null));
                    }
                }

                var currentValue = fieldInfo.GetValue(property.serializedObject.targetObject);
                if (currentValue != null)
                {
                    for (int i = 0; i < values.Count; i++)
                    {
                        if (currentValue.Equals(values[i]))
                        {
                            foundValue = true;
                            selectedIndex = i;
                        }
                    }
                }

                if (!foundValue)
                {
                    var value = fieldInfo.GetValue(property.serializedObject.targetObject);
                    var valueString = value == null ? "NULL" : value.ToString();
                    names.Add("ERROR: NO " + valueString);
                    values.Add(value);
                }

                this.names = names.ToArray();
                this.values = values.ToArray();
            }
        }
    }
}
