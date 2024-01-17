using AsmdefHelper.CustomCreate.Editor;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

namespace RicTools.Editor.Utilities
{
    public static class FileUtilities
    {
        public static Object CreateScriptAssetFromTemplate(string pathName, string resourceFile)
        {
            return CreateScriptAssetFromTemplate(pathName, resourceFile, null);
        }

        public static Object CreateScriptAssetFromTemplate(string pathName, string resourceFile, System.Func<string, string> customReplace)
        {
            string content = File.ReadAllText(resourceFile);
            var method = typeof(ProjectWindowUtil).GetMethod("CreateScriptAssetWithContent", BindingFlags.Static | BindingFlags.NonPublic);
            return (Object)method.Invoke(null, new object[] { pathName, PreprocessScriptAssetTemplate(pathName, content, customReplace) });
        }

        // https://github.com/Unity-Technologies/UnityCsReference/blob/master/Editor/Mono/ProjectWindow/ProjectWindowUtil.cs
        private static string PreprocessScriptAssetTemplate(string pathName, string resourceContent, System.Func<string, string> customReplace)
        {
            string rootNamespace = null;

            if (Path.GetExtension(pathName) == ".cs")
            {
                rootNamespace = CompilationPipeline.GetAssemblyRootNamespaceFromScriptPath(pathName);
            }

            string content = resourceContent;

            // #NOTRIM# is a special marker that is used to mark the end of a line where we want to leave whitespace. prevent editors auto-stripping it by accident.
            content = content.Replace("#NOTRIM#", "");

            // macro replacement
            string baseFile = Path.GetFileNameWithoutExtension(pathName);

            content = content.Replace("#NAME#", baseFile);
            string baseFileNoSpaces = baseFile.Replace(" ", "");
            content = content.Replace("#SCRIPTNAME#", baseFileNoSpaces);

            if (customReplace != null)
            {
                content = customReplace(content);
            }

            content = RemoveOrInsertNamespace(content, rootNamespace);

            // if the script name begins with an uppercase character we support a lowercase substitution variant
            if (char.IsUpper(baseFileNoSpaces, 0))
            {
                baseFileNoSpaces = char.ToLower(baseFileNoSpaces[0]) + baseFileNoSpaces.Substring(1);
                content = content.Replace("#SCRIPTNAME_LOWER#", baseFileNoSpaces);
            }
            else
            {
                // still allow the variant, but change the first character to upper and prefix with "my"
                baseFileNoSpaces = "my" + char.ToUpper(baseFileNoSpaces[0]) + baseFileNoSpaces.Substring(1);
                content = content.Replace("#SCRIPTNAME_LOWER#", baseFileNoSpaces);
            }

            return content;
        }

        private static string RemoveOrInsertNamespace(string content, string rootNamespace)
        {
            var rootNamespaceBeginTag = "#ROOTNAMESPACEBEGIN#";
            var rootNamespaceEndTag = "#ROOTNAMESPACEEND#";

            if (!content.Contains(rootNamespaceBeginTag) || !content.Contains(rootNamespaceEndTag))
                return content;

            if (string.IsNullOrEmpty(rootNamespace))
            {
                content = Regex.Replace(content, $"((\\r\\n)|\\n)[ \\t]*{rootNamespaceBeginTag}[ \\t]*", string.Empty);
                content = Regex.Replace(content, $"((\\r\\n)|\\n)[ \\t]*{rootNamespaceEndTag}[ \\t]*", string.Empty);

                return content;
            }

            // Use first found newline character as newline for entire file after replace.
            var newline = content.Contains("\r\n") ? "\r\n" : "\n";
            var contentLines = new List<string>(content.Split(new[] { "\r\n", "\r", "\n" }, System.StringSplitOptions.None));

            int i = 0;

            for (; i < contentLines.Count; ++i)
            {
                if (contentLines[i].Contains(rootNamespaceBeginTag))
                    break;
            }

            var beginTagLine = contentLines[i];

            // Use the whitespace between beginning of line and #ROOTNAMESPACEBEGIN# as identation.
            var indentationString = beginTagLine.Substring(0, beginTagLine.IndexOf("#"));

            contentLines[i] = $"namespace {rootNamespace}";
            contentLines.Insert(i + 1, "{");

            i += 2;

            for (; i < contentLines.Count; ++i)
            {
                var line = contentLines[i];

                if (string.IsNullOrEmpty(line) || line.Trim().Length == 0)
                    continue;

                if (line.Contains(rootNamespaceEndTag))
                {
                    contentLines[i] = "}";
                    break;
                }

                contentLines[i] = $"{indentationString}{line}";
            }

            return string.Join(newline, contentLines.ToArray());
        }

        public static void CreateNewScript(string defaultNewFileName, string templatePath)
        {
            CreateNewScript<DoCreateScriptAsset>(defaultNewFileName, templatePath);
        }

        public static void CreateNewScript<T>(string defaultNewFileName, string templatePath) where T : EndNameEditAction
        {
            var endAction = ScriptableObject.CreateInstance<T>();
            CreateNewScript(endAction, defaultNewFileName, templatePath);
        }

        public static void CreateNewScript<T>(T endAction, string defaultNewFileName, string templatePath) where T : EndNameEditAction
        {
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(icon: Path.GetExtension(defaultNewFileName) switch
            {
                ".cs" => EditorGUIUtility.IconContent("cs Script Icon").image as Texture2D,
                ".shader" => EditorGUIUtility.IconContent("Shader Icon").image as Texture2D,
                ".asmdef" => EditorGUIUtility.IconContent("AssemblyDefinitionAsset Icon").image as Texture2D,
                ".asmref" => EditorGUIUtility.IconContent("AssemblyDefinitionReferenceAsset Icon").image as Texture2D,
                _ => EditorGUIUtility.IconContent("TextAsset Icon").image as Texture2D,
            }, instanceID: 0, endAction: endAction, pathName: defaultNewFileName, resourceFile: templatePath);
            AssetDatabase.Refresh();
        }

        public static void CreateAsmDef(AssemblyDefinitionJson asmdef, string filePath, bool refreshDatabase = true)
        {
            var asmdefJson = JsonUtility.ToJson(asmdef, true);
            var asmdefPath = $"{filePath}/{asmdef.name}.asmdef";
            File.WriteAllText(asmdefPath, asmdefJson, Encoding.UTF8);
            if (refreshDatabase)
                AssetDatabase.Refresh();
        }
    }
}
