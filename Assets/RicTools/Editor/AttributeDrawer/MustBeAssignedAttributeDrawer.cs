using RicTools.Attributes;
using RicTools.Editor.Utilities;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace RicTools.Editor.AttributeDrawer
{
    [CustomPropertyDrawer(typeof(MustBeAssignedAttribute))]
    public class MustBeAssignedAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (MustBeAssignedCheck.GetError(fieldInfo, property.serializedObject.targetObject) > 0)
            {
                EditorGUIHelper.DrawBox(position, Color.red);
            }

            EditorGUI.PropertyField(position, property, label);
        }
    }

    [InitializeOnLoad]
    internal class MustBeAssignedCheck : UnityEditor.AssetModificationProcessor
    {
        static MustBeAssignedCheck()
        {
            Assert();
        }

        public static string[] OnWillSaveAssets(string[] paths)
        {
            if (paths.Length == 1 && (paths[0] == null || paths[0].EndsWith(".prefab"))) return paths;

            Assert();

            return paths;
        }

        private static void Assert()
        {
            var behaviours = Object.FindObjectsOfType<MonoBehaviour>(true);

            foreach (var behaviour in behaviours)
            {
                if (behaviour == null) continue;

                var objType = behaviour.GetType();
                var fields = objType.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                foreach (var field in fields)
                {
                    if (!field.IsDefined(typeof(MustBeAssignedAttribute), false) || !field.IsDefined(typeof(SerializeField), false)) continue;

                    var error = GetError(field, behaviour);

                    switch (error)
                    {
                        case 1:
                            Debug.LogError($"{objType.Name}: '{field.Name}' is Value Type with default value", behaviour);
                            break;
                        case 2:
                            Debug.LogError($"{objType.Name}: '{field.Name}' not assigned (null value)", behaviour);
                            break;
                        case 3:
                            Debug.LogError($"{objType.Name}: '{field.Name}' not assigned (empty string)", behaviour);
                            break;
                        case 4:
                            Debug.LogError($"{objType.Name}: '{field.Name}' not assigned (empty array)", behaviour);
                            break;
                    }
                }
            }
        }

        public static int GetError(FieldInfo field, Object targetObject)
        {
            var value = field.GetValue(targetObject);
            bool valueTypeWithDefaultValue = field.FieldType.IsValueType && System.Activator.CreateInstance(field.FieldType).Equals(value);
            if (valueTypeWithDefaultValue)
            {
                return 1;
            }


            bool nullReferenceType = value == null || value.Equals(null);
            if (nullReferenceType)
            {
                return 2;
            }


            bool emptyString = field.FieldType == typeof(string) && (string)value == string.Empty;
            if (emptyString)
            {
                return 3;
            }


            var arr = value as System.Array;
            bool emptyArray = arr != null && arr.Length == 0;
            if (emptyArray)
            {
                return 4;
            }

            return 0;
        }
    }
}
