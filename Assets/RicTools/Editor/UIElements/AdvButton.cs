using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace RicTools.Editor.UIElements
{
    // web* src: https://gist.github.com/andrew-raphael-lukasik/69c7858e39e22f197ca51b318b218cc7
    [Preserve]
    public class AdvButton : Button
    {
        public bool enabled
        {
            get => enabledSelf;
            set => SetEnabled(value);
        }
        public new class UxmlFactory : UxmlFactory<AdvButton, UxmlTraits> { }
        public new class UxmlTraits : Button.UxmlTraits
        {
            private UxmlBoolAttributeDescription enabledAttr = new UxmlBoolAttributeDescription { name = "enabled", defaultValue = true };
            public override void Init(VisualElement ve, IUxmlAttributes attributes, CreationContext context)
            {
                base.Init(ve, attributes, context);
                var instance = (AdvButton)ve;
                instance.enabled = enabledAttr.GetValueFromBag(attributes, context);
            }
        }

        public AdvButton() : this(null)
        {

        }

        public AdvButton(System.Action clickEvent) : base(clickEvent)
        {

        }
    }
}
