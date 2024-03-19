using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RicTools;
using RicTools.ScriptableObjects;
using RicTools.EditorAttributes;

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