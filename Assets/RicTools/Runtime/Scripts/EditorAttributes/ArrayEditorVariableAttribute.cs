using RicTools.Utilities;

namespace RicTools.EditorAttributes
{
    public class ArrayEditorVariableAttribute : EditorVariableAttribute
    {
        public bool ShowFoldoutHeader
        {
            get
            {
                if (!ExtraData.TryGetValue("showFoldoutHeader", out var value))
                    return false;
                return (bool)value;
            }
            set
            {
                ExtraData.Set("showFoldoutHeader", value);
            }
        }


        public bool ShowAddRemoveFooter
        {
            get
            {
                if (!ExtraData.TryGetValue("showAddRemoveFooter", out var value))
                    return true;
                return (bool)value;
            }
            set
            {
                ExtraData.Set("showAddRemoveFooter", value);
            }
        }


        public bool ShowBorder
        {
            get
            {
                if (!ExtraData.TryGetValue("showBorder", out var value))
                    return true;
                return (bool)value;
            }
            set
            {
                ExtraData.Set("showBorder", value);
            }
        }


        public bool ShowBoundCollectionSize
        {
            get
            {
                if (!ExtraData.TryGetValue("showBoundCollectionSize", out var value))
                    return true;
                return (bool)value;
            }
            set
            {
                ExtraData.Set("showBoundCollectionSize", value);
            }
        }

        public string HeaderTitle
        {
            get
            {
                if (!ExtraData.TryGetValue("headerTitle", out var value))
                    return "";
                return (string)value;
            }
            set
            {
                ExtraData.Set("headerTitle", value);
            }
        }

        public float FixedItemHeight
        {
            get
            {
                if (!ExtraData.TryGetValue("fixedItemHeight", out var value))
                    return 24;
                return (float)value;
            }
            set
            {
                ExtraData.Set("fixedItemHeight", value);
            }
        }
    }
}
