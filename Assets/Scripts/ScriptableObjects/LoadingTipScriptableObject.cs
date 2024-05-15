using RicTools.EditorAttributes;
using RicTools.ScriptableObjects;

namespace ProjectSteppe.ScriptableObjects
{
    [DefaultScriptableObjectName(nameof(tipName))]
    public class LoadingTipScriptableObject : GenericScriptableObject
    {
        [EditorVariable]
        public string tipName;

        [TextBoxEditorVariable]
        public string tipDescription;
    }
}