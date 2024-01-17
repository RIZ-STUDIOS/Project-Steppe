using System;

namespace RicTools.EditorAttributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class DefaultScriptableObjectName : Attribute
    {
        public string FieldName { get; set; }

        public DefaultScriptableObjectName(string fieldName)
        {
            FieldName = fieldName;
        }
    }
}
