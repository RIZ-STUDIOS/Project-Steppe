using UnityEngine;

namespace RicTools.Attributes
{
    public class ReadOnlyAttribute : PropertyAttribute
    {
        public readonly AvailableMode PlayMode;

        public ReadOnlyAttribute(AvailableMode playMode)
        {
            this.PlayMode = playMode;
        }

        public ReadOnlyAttribute() : this(AvailableMode.All)
        {

        }
    }

    public enum AvailableMode
    {
        All,
        Editor,
        Play,
    }
}
