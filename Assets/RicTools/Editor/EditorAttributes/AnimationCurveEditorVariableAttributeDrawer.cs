using System;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace RicTools.Editor.EditorAttributes
{
    public class AnimationCurveEditorVariableAttributeDrawer : EditorVariableAttributeDrawer
    {
        public override Type FieldType => typeof(AnimationCurve);

        public override VisualElement CreateVisualElement(EditorVariableDrawData editorVariableData)
        {
            var curveField = new CurveField();

            curveField.label = editorVariableData.label;
            curveField.value = (AnimationCurve)editorVariableData.value;

            curveField.RegisterValueChangedCallback(callback =>
            {
                editorVariableData.onValueChange?.Invoke(callback.newValue);
            });

            return curveField;
        }
    }
}
