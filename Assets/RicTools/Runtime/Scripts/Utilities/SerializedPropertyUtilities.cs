#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace RicTools.Utilities
{
    public static class SerializedPropertyUtilities
    {
        public static bool IsNumerical(this SerializedProperty property)
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.Float:
                case SerializedPropertyType.Integer:
                    return true;
            }

            return false;
        }

        public static bool IsVectorFloat(this SerializedProperty property)
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.Vector2:
                case SerializedPropertyType.Vector3:
                case SerializedPropertyType.Vector4:
                    return true;
            }

            return false;
        }

        public static bool IsVectorInt(this SerializedProperty property)
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.Vector2Int:
                case SerializedPropertyType.Vector3Int:
                    return true;
            }

            return false;
        }

        public static bool IsVector(this SerializedProperty property)
        {
            return property.IsVectorFloat() || property.IsVectorInt();
        }

        public static bool HandleMinValues(this SerializedProperty property, float minValue)
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.Float:
                case SerializedPropertyType.Integer:
                    return property.HandleMinNumericalValues(minValue);

                case SerializedPropertyType.Vector2:
                case SerializedPropertyType.Vector3:
                case SerializedPropertyType.Vector4:
                    return property.HandleMinVectorValues(new Vector4(minValue, minValue, minValue, minValue));

                case SerializedPropertyType.Vector2Int:
                case SerializedPropertyType.Vector3Int:
                    return property.HandleMinVectorIntValues(new Vector3Int((int)minValue, (int)minValue, (int)minValue));
            }

            return false;
        }

        public static bool HandleMinNumericalValues(this SerializedProperty property, float minValue)
        {
            if (property.propertyType == SerializedPropertyType.Float && property.floatValue < minValue)
            {
                property.floatValue = minValue;
                return true;
            }

            if (property.propertyType == SerializedPropertyType.Integer && property.intValue < minValue)
            {
                property.intValue = (int)minValue;
            }

            return false;
        }

        public static bool HandleMinVectorValues(this SerializedProperty property, Vector4 minValue)
        {
            Vector4 vector = Vector4.zero;
            switch (property.propertyType)
            {
                case SerializedPropertyType.Vector2:
                    vector = property.vector2Value;
                    break;
                case SerializedPropertyType.Vector3:
                    vector = property.vector3Value;
                    break;
                case SerializedPropertyType.Vector4:
                    vector = property.vector4Value;
                    break;
            }

            bool handled = false;
            for (int i = 0; i < 4; ++i)
            {
                if (vector[i] < minValue[i])
                {
                    vector[i] = minValue[i];
                    handled = true;
                }
            }

            switch (property.propertyType)
            {
                case SerializedPropertyType.Vector2:
                    property.vector2Value = vector;
                    break;
                case SerializedPropertyType.Vector3:
                    property.vector3Value = vector;
                    break;
                case SerializedPropertyType.Vector4:
                    property.vector4Value = vector;
                    break;
            }

            return handled;
        }

        public static bool HandleMinVectorIntValues(this SerializedProperty property, Vector3Int minValue)
        {
            Vector3Int vector = Vector3Int.zero;
            switch (property.propertyType)
            {
                case SerializedPropertyType.Vector2Int:
                    vector = (Vector3Int)property.vector2IntValue;
                    break;
                case SerializedPropertyType.Vector3Int:
                    vector = property.vector3IntValue;
                    break;
            }

            bool handled = false;
            for (int i = 0; i < 3; ++i)
            {
                if (vector[i] < minValue[i])
                {
                    vector[i] = minValue[i];
                    handled = true;
                }
            }

            switch (property.propertyType)
            {
                case SerializedPropertyType.Vector2Int:
                    property.vector2IntValue = (Vector2Int)vector;
                    break;
                case SerializedPropertyType.Vector3Int:
                    property.vector3IntValue = vector;
                    break;
            }

            return handled;
        }

        public static bool HandleMaxValues(this SerializedProperty property, float maxValue)
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.Float:
                case SerializedPropertyType.Integer:
                    return property.HandleMaxNumericalValues(maxValue);

                case SerializedPropertyType.Vector2:
                case SerializedPropertyType.Vector3:
                case SerializedPropertyType.Vector4:
                    return property.HandleMaxVectorValues(new Vector4(maxValue, maxValue, maxValue, maxValue));

                case SerializedPropertyType.Vector2Int:
                case SerializedPropertyType.Vector3Int:
                    return property.HandleMaxVectorIntValues(new Vector3Int((int)maxValue, (int)maxValue, (int)maxValue));
            }

            return false;
        }

        public static bool HandleMaxNumericalValues(this SerializedProperty property, float maxValue)
        {
            if (property.propertyType == SerializedPropertyType.Float && property.floatValue > maxValue)
            {
                property.floatValue = maxValue;
                return true;
            }

            if (property.propertyType == SerializedPropertyType.Integer && property.intValue > maxValue)
            {
                property.intValue = (int)maxValue;
            }

            return false;
        }

        public static bool HandleMaxVectorValues(this SerializedProperty property, Vector4 maxValue)
        {
            Vector4 vector = Vector4.zero;
            switch (property.propertyType)
            {
                case SerializedPropertyType.Vector2:
                    vector = property.vector2Value;
                    break;
                case SerializedPropertyType.Vector3:
                    vector = property.vector3Value;
                    break;
                case SerializedPropertyType.Vector4:
                    vector = property.vector4Value;
                    break;
            }

            bool handled = false;
            for (int i = 0; i < 4; ++i)
            {
                if (vector[i] > maxValue[i])
                {
                    vector[i] = maxValue[i];
                    handled = true;
                }
            }

            switch (property.propertyType)
            {
                case SerializedPropertyType.Vector2:
                    property.vector2Value = vector;
                    break;
                case SerializedPropertyType.Vector3:
                    property.vector3Value = vector;
                    break;
                case SerializedPropertyType.Vector4:
                    property.vector4Value = vector;
                    break;
            }

            return handled;
        }

        public static bool HandleMaxVectorIntValues(this SerializedProperty property, Vector3Int maxValue)
        {
            Vector3Int vector = Vector3Int.zero;
            switch (property.propertyType)
            {
                case SerializedPropertyType.Vector2Int:
                    vector = (Vector3Int)property.vector2IntValue;
                    break;
                case SerializedPropertyType.Vector3Int:
                    vector = property.vector3IntValue;
                    break;
            }

            bool handled = false;
            for (int i = 0; i < 3; ++i)
            {
                if (vector[i] > maxValue[i])
                {
                    vector[i] = maxValue[i];
                    handled = true;
                }
            }

            switch (property.propertyType)
            {
                case SerializedPropertyType.Vector2Int:
                    property.vector2IntValue = (Vector2Int)vector;
                    break;
                case SerializedPropertyType.Vector3Int:
                    property.vector3IntValue = vector;
                    break;
            }

            return handled;
        }
    }
}
#endif