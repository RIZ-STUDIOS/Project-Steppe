using ProjectSteppe.ScriptableObjects.CameraData.AimCameraData;
using ProjectSteppe.ScriptableObjects.CameraData.BodyCameraData;
using ProjectSteppe.ScriptableObjects.CameraData.ExtensionCameraData;
using RicTools.Editor.UIElements;
using RicTools.Editor.Utilities;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProjectSteppe.Editor.CameraDataSubEditors
{
    [System.Serializable]
    public abstract class CameraDataSubEditor<T> where T : ScriptableObject
    {
        protected VisualElement rootVisualElement;
        private event System.Action onLoad;

        public CameraDataSubEditor(VisualElement rootVisualElement)
        {
            this.rootVisualElement = new VisualElement();
            rootVisualElement.Add(this.rootVisualElement);
            CreateGUI();
        }

        public abstract bool IsSelectedEditor(int index);

        public abstract T CreateScriptableObject();

        public virtual void Hide()
        {
            rootVisualElement.AddToClassList("hidden");
        }

        public virtual void Show()
        {
            rootVisualElement.RemoveFromClassList("hidden");
        }

        public void CheckVisibility(int index)
        {
            if (IsSelectedEditor(index))
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        protected abstract void CreateGUI();

        public void Load(bool isNull, ScriptableObject asset)
        {
            if (asset && !IsSameType(asset.GetType())) return;
            LoadData(isNull, (T)asset);
            onLoad?.Invoke();
        }

        protected abstract void LoadData(bool isNull, T asset);

        protected void RegisterLoadChange(LayerMaskField element, EditorContainer<LayerMask> editorContainer)
        {
            onLoad += () =>
            {
                element.value = editorContainer.Value;
            };
        }

        protected void RegisterLoadChange<TValueType>(ObjectField element, EditorContainer<TValueType> editorContainer) where TValueType : Object
        {
            onLoad += () =>
            {
                element.value = editorContainer.Value;
            };
        }

        protected void RegisterLoadChange<TValueType>(BaseField<TValueType> element, EditorContainer<TValueType> editorContainer)
        {
            onLoad += () =>
            {
                element.value = editorContainer.Value;
            };
        }

        protected void RegisterLoadChange<TValueType>(RicToolsListView<TValueType> listView, EditorContainerList<TValueType> editorContainerList)
        {
            onLoad += () =>
            {
                listView.UpdateItemSource(editorContainerList);
            };
        }

        protected void RegisterLoadChange<TValueType>(BaseField<System.Enum> element, EditorContainer<TValueType> editorContainer) where TValueType : System.Enum
        {
            onLoad += () =>
            {
                element.value = editorContainer.Value;
            };
        }

        public abstract bool IsSameType(System.Type type);
    }

    [System.Serializable]
    public abstract class BodyCameraDataSubEditor : CameraDataSubEditor<BaseBodyCameraDataScriptableObject>
    {
        public CameraDataEditorWindow.BodyDataType bodyDataType;
        private readonly System.Type componentType;

        protected BodyCameraDataSubEditor(VisualElement rootVisualElement, CameraDataEditorWindow.BodyDataType bodyDataType, System.Type componentType) : base(rootVisualElement)
        {
            this.bodyDataType = bodyDataType;
            this.componentType = componentType;
        }

        public override bool IsSelectedEditor(int index)
        {
            return index == (int)bodyDataType;
        }

        public override bool IsSameType(System.Type type)
        {
            return type == componentType;
        }
    }

    [System.Serializable]
    public abstract class AimCameraDataSubEditor : CameraDataSubEditor<BaseAimCameraDataScriptableObject>
    {
        public CameraDataEditorWindow.AimDataType aimDataType;
        private readonly System.Type componentType;

        protected AimCameraDataSubEditor(VisualElement rootVisualElement, CameraDataEditorWindow.AimDataType aimDataType, System.Type componentType) : base(rootVisualElement)
        {
            this.aimDataType = aimDataType;
            this.componentType = componentType;
        }

        public override bool IsSelectedEditor(int index)
        {
            return index == (int)aimDataType;
        }

        public override bool IsSameType(System.Type type)
        {
            return type == componentType;
        }
    }

    [System.Serializable]
    public abstract class ExtensionCameraDataSubEditor : CameraDataSubEditor<BaseExtensionCameraDataScriptableObject>
    {
        public CameraDataEditorWindow.ExtensionDataType extensionDataType;
        private readonly System.Type componentType;

        protected ExtensionCameraDataSubEditor(VisualElement rootVisualElement, CameraDataEditorWindow.ExtensionDataType extensionDataType, System.Type componentType) : base(rootVisualElement)
        {
            this.extensionDataType = extensionDataType;
            this.componentType = componentType;
        }

        public override bool IsSelectedEditor(int index)
        {
            return index == (int)extensionDataType;
        }

        public override bool IsSameType(System.Type type)
        {
            return type == componentType;
        }
    }
}
