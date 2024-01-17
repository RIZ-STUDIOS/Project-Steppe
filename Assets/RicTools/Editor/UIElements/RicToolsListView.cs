using RicTools.Editor.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace RicTools.Editor.UIElements
{
    public abstract class RicToolsListView<TValueType>
    {
        public readonly ListView listView = new ListView();
        protected IList<TValueType> itemsSource => (IList<TValueType>)listView.itemsSource;

        private readonly System.Func<TValueType> getDefaultValue;

        public const int ITEM_HEIGHT = 24;

        public RicToolsListView(EditorContainerList<TValueType> editorContainerList, Func<TValueType> getDefaultValue)
        {
            this.getDefaultValue = getDefaultValue;
            listView.showAddRemoveFooter = true;
            listView.showBorder = true;
            listView.showFoldoutHeader = true;
            listView.headerTitle = "Header";
            listView.showBoundCollectionSize = true;
            listView.fixedItemHeight = ITEM_HEIGHT;
            listView.makeItem += () =>
            {
                var root = new VisualElement();

                root.userData = -1;

                MakeItem(root);

                return root;
            };
            listView.bindItem += (root, index) =>
            {
                root.userData = index;
                BindItem(root, index);
            };
            listView.itemsAdded += ItemsAdded;
            listView.unbindItem += UnbindItem;
            UpdateItemSource(editorContainerList);
        }

        protected abstract void MakeItem(VisualElement root);

        protected abstract void BindItem(VisualElement root, int index);

        protected virtual void ItemsAdded(IEnumerable<int> indexes)
        {
            foreach (var index in indexes)
            {
                itemsSource[index] = getDefaultValue();
            }
        }

        protected virtual void UnbindItem(VisualElement root, int index)
        {

        }

        public void UpdateItemSource(EditorContainerList<TValueType> editorContainerList)
        {
            listView.itemsSource = editorContainerList.List;
            listView.Rebuild();
        }

        public void CalculateItemHeight(int itemCount)
        {
            if (itemCount < 1) itemCount = 1;
            listView.fixedItemHeight = itemCount * ITEM_HEIGHT;
        }
    }

    public sealed class BasicListView<TValueType, VisualElementType> : RicToolsListView<TValueType> where VisualElementType : VisualElement, INotifyValueChanged<TValueType>, new()
    {
        public BasicListView(EditorContainerList<TValueType> editorContainerList) : this(editorContainerList, () => default)
        {

        }

        public BasicListView(EditorContainerList<TValueType> editorContainerList, Func<TValueType> getDefaultValue) : base(editorContainerList, getDefaultValue)
        {
        }

        protected override void BindItem(VisualElement root, int index)
        {
            root.Q<VisualElementType>().value = itemsSource[index];
        }

        protected override void ItemsAdded(IEnumerable<int> indexes)
        {
        }

        protected override void MakeItem(VisualElement root)
        {
            root.userData = -1;

            var element = new VisualElementType();
            root.Add(element);
            element.RegisterValueChangedCallback(callback =>
            {
                var index = (int)root.userData;
                if (index < 0) return;
                itemsSource[index] = callback.newValue;
            });
        }
    }
}
