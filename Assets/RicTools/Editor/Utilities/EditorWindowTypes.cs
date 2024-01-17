using RicTools.Editor.EditorAttributes;
using System;
using System.Linq;
using UnityEditor;

namespace RicTools.Editor.Utilities
{
    internal static class EditorWindowTypes
    {
        public static bool HasTypeToVisualElement(Type type)
        {
            return GetVisualElementType(type) != null;
        }

        public static EditorVariableAttributeDrawer GetVisualElementType(Type type)
        {
            var types = TypeCache.GetTypesDerivedFrom(typeof(EditorVariableAttributeDrawer)).ToList();

            foreach (var typeValuePair in types)
            {
                var drawer = (EditorVariableAttributeDrawer)Activator.CreateInstance(typeValuePair);
                if (type.IsSubclassOf(drawer.FieldType) || type == drawer.FieldType)
                    return drawer;
            }

            return null;
        }
    }
}
