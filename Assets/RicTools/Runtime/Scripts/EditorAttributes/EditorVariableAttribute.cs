using System;
using System.Collections.Generic;

namespace RicTools.EditorAttributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class EditorVariableAttribute : Attribute
    {
        public string Label { get; set; }
        public object DefaultValue { get; set; }
        public Dictionary<string, object> ExtraData { get; set; } = new Dictionary<string, object>();
    }
}
