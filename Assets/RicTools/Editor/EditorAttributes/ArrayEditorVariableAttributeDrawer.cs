using RicTools.Editor.Utilities;
using RicTools.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace RicTools.Editor.EditorAttributes
{
    public class ArrayEditorVariableAttributeDrawer : EditorVariableAttributeDrawer
    {
        public override Type FieldType => typeof(Array);

        public override VisualElement CreateVisualElement(EditorVariableDrawData data)
        {
            var extraType = data.extraData;

            if (!extraType.TryGetValue("showFoldoutHeader", out var showFoldoutHeader))
                showFoldoutHeader = false;

            if (!extraType.TryGetValue("headerTitle", out var headerTitle))
                headerTitle = "Header";

            if (!extraType.TryGetValue("showAddRemoveFooter", out var showAddRemoveFooter))
                showAddRemoveFooter = true;

            if (!extraType.TryGetValue("showBorder", out var showBorder))
                showBorder = true;

            if (!extraType.TryGetValue("showBoundCollectionSize", out var showBoundCollectionSize))
                showBoundCollectionSize = true;

            if (!extraType.TryGetValue("fixedItemHeight", out var fixedItemHeight))
                fixedItemHeight = 24;

            if (fixedItemHeight.GetType() != typeof(float))
                fixedItemHeight = Convert.ToSingle(fixedItemHeight);

            var listView = new ListView();
            listView.headerTitle = (string)headerTitle;
            listView.showFoldoutHeader = (bool)showFoldoutHeader;
            listView.showAddRemoveFooter = (bool)showAddRemoveFooter;
            listView.showBorder = (bool)showBorder;
            listView.showBoundCollectionSize = (bool)showBoundCollectionSize;
            listView.itemsSource = (IList)data.value.Copy();

            listView.fixedItemHeight = (float)fixedItemHeight;

            listView.makeItem += () =>
            {
                var root = new VisualElement();


                var type = EditorWindowTypes.GetVisualElementType(data.innerType);
                var visualElement = type.CreateVisualElement(new EditorVariableDrawData("", data.innerType.GetDefaultValue(), data.innerType, new Dictionary<string, object>(), null)
                {
                    onValueChange = (data) =>
                    {
                        var index = (int)root.userData;
                        if (index < 0) return;

                        listView.itemsSource[index] = data;
                    }
                });
                visualElement.name = "Editor Variable Visual Element";
                root.Add(visualElement);

                root.userData = -1;

                return root;
            };

            listView.bindItem += (root, index) =>
            {
                root.userData = index;
                var type = EditorWindowTypes.GetVisualElementType(data.innerType);
                var visualElement = root.Q("Editor Variable Visual Element");
                var labelProperty = visualElement.GetType().GetProperty("label");
                if (labelProperty != null)
                    labelProperty.SetValue(visualElement, $"Element {index}");
                type.LoadData(visualElement, listView.itemsSource[index]);
            };

            listView.itemsAdded += (indexes) =>
            {
                foreach (var index in indexes)
                {
                    listView.itemsSource[index] = data.innerType.GetDefaultValue();
                }
            };

            return listView;
        }

        public override void LoadData(VisualElement visualElement, object data)
        {
            var listView = (ListView)visualElement;
            listView.itemsSource = (IList)data;
            listView.Rebuild();
        }

        public override object SaveData(VisualElement visualElement, Type fieldType)
        {
            var listView = (ListView)visualElement;
            var list = Activator.CreateInstance(typeof(List<>).MakeGenericType(fieldType));

            foreach (var item in listView.itemsSource)
            {
                list.GetType().GetMethod("Add").Invoke(list, new object[] { item });
            }

            if (fieldType.IsList())
            {
                return list.Copy();
            }
            return list.GetType().GetMethod("ToArray").Invoke(list, null);
        }
    }
}
