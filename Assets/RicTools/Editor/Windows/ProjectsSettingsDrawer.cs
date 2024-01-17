using RicTools.Editor.Settings;
using RicTools.Managers;
using RicTools.ScriptableObjects;
using RicTools.Settings;
using RicTools.Utilities;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

namespace RicTools.Editor.Windows
{
    internal static class ProjectsSettingsDrawer
    {
        internal class Styles
        {
            public static readonly GUIContent scriptableEditorsLabel = new GUIContent("Scriptable Editors");
            public static readonly GUIContent scriptableEditorsListLabel = new GUIContent("Scriptable Editors List");
            public static readonly GUIContent singletonManagersLabel = new GUIContent("Singleton Managers");
            public static readonly GUIContent singletonManagersListLabel = new GUIContent("Singleton Managers List");

            public static readonly GUIContent singletonPrefabManagersLabel = new GUIContent("Singleton Managers");
            public static readonly GUIContent singletonPrefabManagersListLabel = new GUIContent("Singleton Managers List");

            public static readonly GUIContent scriptableEditorsAddButtonLabel = new GUIContent("Add Scriptable Editor");
        }

        private static ReorderableList m_singletonManagersList;
        private static ReorderableList m_singletonPrefabManagersList;

        private static SerializedObject editorSerializedObject;
        private static SerializedObject serializedObject;

        private static RicTools_RuntimeSettings settings;
        private static RicTools_EditorSettings editorSettings;

        private const string k_UndoRedo = "UndoRedoPerformed";

        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            return new SettingsProvider("Project/Packages/RicTools", SettingsScope.Project)
            {
                activateHandler = OnActivate,
                guiHandler = OnGUI,
                keywords = GetKeywords(),
                titleBarGuiHandler = () =>
                {
                    EditorGUILayout.LabelField("Version: " + RicTools_EditorSettings.version, new GUIStyle(GUI.skin.label)
                    {
                        alignment = TextAnchor.MiddleRight,
                    });
                }
            };
        }

        private static void OnActivate(string searchContext, VisualElement rootElement)
        {
            editorSettings = RicTools_EditorSettings.instance;
            settings = RicTools_RuntimeSettings.instance;

            editorSerializedObject = RicTools_EditorSettings.GetSerializedObject();
            serializedObject = RicTools_RuntimeSettings.GetSerializedObject();

            m_singletonPrefabManagersList = new ReorderableList(serializedObject, serializedObject.FindProperty("m_singletonPrefabManagers"), false, true, true, true);

            m_singletonManagersList = new ReorderableList(serializedObject, serializedObject.FindProperty("m_singletonManagers"), false, true, true, true);

            m_singletonManagersList.elementHeightCallback = index =>
            {
                var manager = settings.m_singletonManagers[index].manager.Type;
                if (RicUtilities.IsSubclassOfRawGeneric(typeof(DataGenericManager<,>), manager))
                {
                    return 42;
                }
                return 21;
            };

            m_singletonPrefabManagersList.elementHeightCallback = index =>
            {
                return 21;
            };

            m_singletonPrefabManagersList.drawElementCallback = (rect, index, isActive, isFocused) =>
            {
                var element = m_singletonPrefabManagersList.serializedProperty.GetArrayElementAtIndex(index);
                rect.y += 2;
                float labelWidth = 200;
                float width = rect.width - labelWidth;

                EditorGUI.LabelField(new Rect(rect.x, rect.y, labelWidth, EditorGUIUtility.singleLineHeight), "Prefab");
                //rect.x += 110;
                EditorGUI.PropertyField(new Rect(rect.x + labelWidth, rect.y, width, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("prefab"), GUIContent.none);
            };

            m_singletonManagersList.drawElementCallback = (rect, index, isActive, isFocused) =>
            {
                var element = m_singletonManagersList.serializedProperty.GetArrayElementAtIndex(index);
                rect.y += 2;
                float labelWidth = 200;
                float width = rect.width - labelWidth;

                EditorGUI.LabelField(new Rect(rect.x, rect.y, labelWidth, EditorGUIUtility.singleLineHeight), "Manager");
                //rect.x += 110;
                EditorGUI.PropertyField(new Rect(rect.x + labelWidth, rect.y, width, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("manager"), GUIContent.none);

                var manager = settings.m_singletonManagers[index].manager.Type;
                if (RicUtilities.IsSubclassOfRawGeneric(typeof(DataGenericManager<,>), manager))
                {
                    rect.y += EditorGUIUtility.singleLineHeight + 2;
                    EditorGUI.LabelField(new Rect(rect.x, rect.y, labelWidth, EditorGUIUtility.singleLineHeight), "Data");
                    var dataType = manager.BaseType.GenericTypeArguments[1];
                    if (settings.m_singletonManagers[index].data != null && settings.m_singletonManagers[index].data.GetType() != dataType)
                    {
                        settings.m_singletonManagers[index].data = null;
                    }
                    GUI.enabled = settings.m_singletonManagers[index].data == null;
                    if (settings.m_singletonManagers[index].data == null)
                    {
                        if (GUI.Button(new Rect(rect.x + labelWidth, rect.y, width, EditorGUIUtility.singleLineHeight), "Create"))
                        {
                            RicUtilities.CreateAssetFolder(PathConstants.MANAGERS_DATA_PATH);

                            var data = ScriptableObject.CreateInstance(manager.BaseType.GenericTypeArguments[1]);
                            if (!AssetDatabase.Contains(data))
                                AssetDatabase.CreateAsset(data, $"{PathConstants.MANAGERS_DATA_PATH}/{manager.Name}_data.asset");

                            settings.m_singletonManagers[index].data = data as DataManagerScriptableObject;

                            EditorUtility.SetDirty(settings);
                            AssetDatabase.SaveAssets();
                        }
                    }
                    else
                    {
                        EditorGUI.PropertyField(new Rect(rect.x + labelWidth, rect.y, width, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("data"), GUIContent.none);
                    }
                    GUI.enabled = true;
                }
            };

            m_singletonManagersList.drawHeaderCallback = rect =>
            {
                EditorGUI.LabelField(rect, Styles.singletonManagersListLabel);
            };

            m_singletonPrefabManagersList.drawHeaderCallback = rect =>
            {
                EditorGUI.LabelField(rect, Styles.singletonPrefabManagersListLabel);
            };
        }

        private static void OnGUI(string searchContext)
        {
            editorSerializedObject.Update();
            serializedObject.Update();
            string evt_cmd = Event.current.commandName;

            float labelWidth = EditorGUIUtility.labelWidth;
            float fieldWidth = EditorGUIUtility.fieldWidth;
            EditorGUI.indentLevel = 0;

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            GUILayout.Label(Styles.singletonManagersLabel, EditorStyles.boldLabel);
            m_singletonManagersList.DoLayoutList();

            EditorGUI.indentLevel = 0;

            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            EditorGUI.indentLevel = 0;

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            GUILayout.Label(Styles.singletonPrefabManagersLabel, EditorStyles.boldLabel);
            m_singletonPrefabManagersList.DoLayoutList();

            EditorGUI.indentLevel = 0;

            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();

            if (editorSerializedObject.ApplyModifiedProperties() || evt_cmd == k_UndoRedo)
            {
                EditorUtility.SetDirty(editorSettings);
                //TMPro_EventManager.ON_TMP_SETTINGS_CHANGED();
            }

            if (serializedObject.ApplyModifiedProperties() || evt_cmd == k_UndoRedo)
            {
                EditorUtility.SetDirty(settings);
                //TMPro_EventManager.ON_TMP_SETTINGS_CHANGED();
            }
        }

        private static HashSet<string> GetKeywords()
        {
            var keywords = new HashSet<string>();
            keywords.AddWords(Styles.scriptableEditorsLabel.text);
            keywords.AddWords(Styles.singletonManagersLabel.text);
            return keywords;
        }

        private static readonly char[] _separators = { ' ' };

        private static void AddWords(this HashSet<string> set, string phrase)
        {
            foreach (string word in phrase.Split(_separators))
            {
                set.Add(word);
            }
        }
    }
}
