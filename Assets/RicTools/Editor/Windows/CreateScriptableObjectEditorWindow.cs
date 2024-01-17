using RicTools.Editor.Utilities;
using RicTools.Utilities;
using System.IO;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.UIElements;

namespace RicTools.Editor.Windows
{
    internal class CreateScriptableObjectEditorWindow : EditorWindow
    {
        [SerializeField]
        private EditorContainer<string> scriptableObjectName = new EditorContainer<string>();

        [SerializeField]
        private EditorContainer<string> editorWindowName = new EditorContainer<string>();

        [SerializeField]
        private EditorContainer<bool> openInEditor = new EditorContainer<bool>();

        [SerializeField]
        private EditorContainer<string> windowName = new EditorContainer<string>();

        [SerializeField]
        private EditorContainer<string> menuItem = new EditorContainer<string>("Window/RicTools Windows");

        private TextField soNameTextField;
        private TextField editorNameTextField;
        private TextField windowNameTextField;

        private VisualElement emptyFieldWarningContainer;

        public bool useCurrentProjectLocation;

        private void OnEnable()
        {
            titleContent = new GUIContent("RicTools Scriptable Object Creator");

            maxSize = new Vector2(400, 166);
            minSize = maxSize;

            EditorPrefs.SetBool("ReadyToUpdateSettings", false);
        }

        private void CreateGUI()
        {
            rootVisualElement.AddCommonStylesheet();

            rootVisualElement.AddLabel("Assets To Create");
            rootVisualElement.AddSeparator(new Color32(37, 37, 37, 255));

            EventCallback<FocusEvent> focusEvent = (callback) =>
            {
                ToggleWarning(false);
            };

            soNameTextField = rootVisualElement.AddTextField(scriptableObjectName, "Scriptable Object", (old) =>
            {
                if (editorWindowName.Value == null || editorWindowName.Value == old)
                {
                    editorWindowName.Value = scriptableObjectName;
                }

                UpdateTextFields();
            });
            editorNameTextField = rootVisualElement.AddTextField(editorWindowName, "Editor Window", (old) =>
            {
                if (windowName.Value == null || windowName.Value == old)
                {
                    windowName.Value = editorWindowName;
                }

                UpdateTextFields();
            });

            rootVisualElement.AddSeparator();

            windowNameTextField = rootVisualElement.AddTextField(windowName, "Window Name");

            {
                var element = rootVisualElement.AddTextField(menuItem, "Menu Location");
                element.RegisterCallback(focusEvent);
            }

            {
                soNameTextField.RegisterCallback(focusEvent);
                editorNameTextField.RegisterCallback(focusEvent);
                windowNameTextField.RegisterCallback(focusEvent);
            }

            {
                emptyFieldWarningContainer = new VisualElement();

                emptyFieldWarningContainer.AddLabel("Cannot have empty fields");

                rootVisualElement.Add(emptyFieldWarningContainer);

                ToggleWarning(false);
            }

            rootVisualElement.AddToggle(openInEditor, "Open in editor when files are created");

            rootVisualElement.AddButton("Create", CreateAssets);

            soNameTextField.Focus();
        }

        private void UpdateTextFields()
        {
            soNameTextField.value = scriptableObjectName.Value;
            editorNameTextField.value = editorWindowName.Value;
            windowNameTextField.value = windowName.Value;
        }

        private void CreateAssets()
        {
            if (string.IsNullOrWhiteSpace(scriptableObjectName.Value) || string.IsNullOrWhiteSpace(editorWindowName.Value) || string.IsNullOrEmpty(this.windowName) || string.IsNullOrEmpty(menuItem))
            {
                ToggleWarning(true);
                return;
            }

            Close();

            EditorPrefs.SetBool("ReadyToUpdateSettings", true);

            var path = ToolUtilities.GetSelectedPathOrFallback();
            if (useCurrentProjectLocation)
            {
                path = ToolUtilities.GetUniquePathNameAtSelectedPath("test.txt");
                path = Path.GetDirectoryName(path);
            }

            string soName = scriptableObjectName + "ScriptableObject";
            string editorWindow = editorWindowName + "EditorWindow";
            string windowName = this.windowName;
            string menuLocation = menuItem;

            string rootNamespace = CompilationPipeline.GetAssemblyRootNamespaceFromScriptPath(path + "/temp.cs");

            var dll = CompilationPipeline.GetAssemblyNameFromScriptPath(path + "/temp.cs").Split('.')[0];

            if (!string.IsNullOrEmpty(rootNamespace)) { rootNamespace += "."; }

            Object genericSoFile;
            Object editorWindowFile;

            {

                string defaultNewFileName = Path.Combine(path, soName + ".cs");

                string templatePath = PathConstants.TEMPLATES_PATH + "/Script-NewGenericScriptableObject.cs.txt";

                genericSoFile = FileUtilities.CreateScriptAssetFromTemplate(defaultNewFileName, templatePath);
            }

            {
                RicUtilities.CreateAssetFolder("Assets/Editor");

                string defaultNewFileName = Path.Combine("Assets/Editor", editorWindow + ".cs");

                rootNamespace = CompilationPipeline.GetAssemblyRootNamespaceFromScriptPath("Assets/Editor/temp.cs");

                string templatePath = PathConstants.TEMPLATES_PATH + "/Script-NewGenericEditorWindow.cs.txt";

                editorWindowFile = FileUtilities.CreateScriptAssetFromTemplate(defaultNewFileName, templatePath, (content) =>
                {
                    content = content.Replace("#SCRIPTABLEOBJECT#", soName);
                    content = content.Replace("#WINDOWNAME#", windowName);
                    content = content.Replace("#MENULOCATION#", menuLocation);
                    return content;
                });
            }

            if (openInEditor.Value)
            {
                AssetDatabase.OpenAsset(genericSoFile);
                AssetDatabase.OpenAsset(editorWindowFile);
            }
        }

        private void ToggleWarning(bool visible)
        {
            emptyFieldWarningContainer.ToggleClass("hidden", !visible);
        }
    }
}
