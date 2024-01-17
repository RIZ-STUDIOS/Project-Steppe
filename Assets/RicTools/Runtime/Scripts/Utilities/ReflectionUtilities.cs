using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RicTools.Utilities
{
    public static class ReflectionUtilities
    {
        public static IEnumerable<MethodInfo> GetMethodsRecursive(this Type type, BindingFlags bindingFlags)
        {
            IEnumerable<MethodInfo> methods = type.GetMethods(bindingFlags);

            if (type.BaseType != null)
            {
                methods = methods.Concat(type.BaseType.GetMethodsRecursive(bindingFlags));
            }

            return methods;
        }

        public static MethodInfo GetMethodRecursive(this Type type, string name, BindingFlags bindingFlags)
        {
            var method = type.GetMethod(name, bindingFlags);

            if (method == null && type.BaseType != null)
            {
                method = type.BaseType.GetMethodRecursive(name, bindingFlags);
            }

            return method;
        }

        public static IEnumerable<FieldInfo> GetFieldsRecursive(this Type type, BindingFlags bindingFlags)
        {
            IEnumerable<FieldInfo> methods = type.GetFields(bindingFlags);

            if (type.BaseType != null)
            {
                methods = methods.Concat(type.BaseType.GetFieldsRecursive(bindingFlags));
            }

            return methods;
        }

        public static FieldInfo GetFieldRecursive(this Type type, string name, BindingFlags bindingFlags)
        {
            var method = type.GetField(name, bindingFlags);

            if (method == null && type.BaseType != null)
            {
                method = type.BaseType.GetFieldRecursive(name, bindingFlags);
            }

            return method;
        }

        public static object GetDefaultValue(this Type type)
        {
            if (type.IsEnum)
            {
                return type.GetEnumValues().GetValue(0);
            }
            else if (type.IsArray)
            {
                var innerType = type.GetElementType();
                return System.Activator.CreateInstance(typeof(List<>).MakeGenericType(innerType));
            }
            else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                var innerType = type.GetGenericArguments()[0];
                return System.Activator.CreateInstance(typeof(List<>).MakeGenericType(innerType));
            }
            else if (type.IsValueType)
            {
                return System.Activator.CreateInstance(type);
            }
            return null;
        }

        public static bool IsList(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);
        }
    }
}
