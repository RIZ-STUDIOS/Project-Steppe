using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace RicTools.Editor.Utilities
{
    [Serializable]
    public sealed class EditorContainer<TValueType> : IConvertible
    {
        public TValueType Value { get; set; } = default;
        private readonly TValueType defaultValue;

        public static implicit operator TValueType(EditorContainer<TValueType> value) { return value.Value; }
        public static explicit operator EditorContainer<TValueType>(TValueType value) { return new EditorContainer<TValueType>() { Value = value }; }

        public EditorContainer() : this(default)
        {
        }

        public EditorContainer(TValueType value)
        {
            Value = value;
            defaultValue = value;
        }

        public void Reset()
        {
            Value = defaultValue;
        }

        public TypeCode GetTypeCode()
        {
            return TypeCode.Object;
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            return false;
        }

        public byte ToByte(IFormatProvider provider)
        {
            return 0;
        }

        public char ToChar(IFormatProvider provider)
        {
            return '\0';
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            return DateTime.MinValue;
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            return 0;
        }

        public double ToDouble(IFormatProvider provider)
        {
            return 0;
        }

        public short ToInt16(IFormatProvider provider)
        {
            return 0;
        }

        public int ToInt32(IFormatProvider provider)
        {
            return 0;
        }

        public long ToInt64(IFormatProvider provider)
        {
            return 0;
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            return 0;
        }

        public float ToSingle(IFormatProvider provider)
        {
            return 0;
        }

        public string ToString(IFormatProvider provider)
        {
            return string.Empty;
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            if (!conversionType.IsGenericType) return null;
            if (conversionType.GetGenericTypeDefinition() != typeof(EditorContainer<>)) return null;
            var editorContainer = Activator.CreateInstance(typeof(EditorContainer<>).MakeGenericType(conversionType.GenericTypeArguments[0]));
            editorContainer.GetType().GetProperty("Value").SetValue(editorContainer, Value);
            return editorContainer;
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            return 0;
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            return 0;
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            return 0;
        }
    }

    [Serializable]
    public sealed class EditorContainerList<TValueType>
    {
        internal List<TValueType> List { get; set; } = new List<TValueType>();

        public void Load(IEnumerable<TValueType> list)
        {
            List = new List<TValueType>(list.Copy());
        }

        public void Clear()
        {
            List.Clear();
        }

        public TValueType[] ToArray()
        {
            return List.ToArray();
        }

        public List<TValueType> ToList()
        {
            return List.Copy();
        }
    }
}