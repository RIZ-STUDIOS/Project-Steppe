using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RicTools.Editor")]
namespace RicTools
{
    internal static class PathConstants
    {
        public const string SCRIPTABLES_FOLDER = "ScriptableObjects/" + RESOURCES_FOLDER;

        public const string RICTOOLS_FOLDER = "RicTools";

        public const string EDITOR_FOLDER = "Editor";

        public const string ASSETS_FOLDER = "Assets";

        public const string RESOURCES_FOLDER = "Resources";

        public const string MANAGERS_DATA_FOLDER = "Managers Data";
        public const string MANAGERS_DATA_PATH = ASSETS_FOLDER + "/" + SCRIPTABLES_FOLDER + "/" + MANAGERS_DATA_FOLDER;

        public const string EDITOR_SETTINGS_PATH = ASSETS_FOLDER + "/" + RICTOOLS_FOLDER + "/" + RESOURCES_FOLDER + "/" + EDITOR_FOLDER;
        public const string RUNTIME_SETTINGS_PATH = ASSETS_FOLDER + "/" + RICTOOLS_FOLDER + "/" + RESOURCES_FOLDER;

        public const string EDITOR_SETTINGS_FILE_PATH = EDITOR_FOLDER + "/" + EDITOR_SETTINGS_NAME;
        public const string EDITOR_SETTINGS_NAME = "RicTools_Editor_Settings";

        public const string RUNTIME_SETTINGS_FILE_PATH = RUNTIME_SETTINGS_NAME;
        public const string RUNTIME_SETTINGS_NAME = "RicTools_Settings";

        public const string TEMPLATES_PATH = ASSETS_FOLDER + "/" + RICTOOLS_FOLDER + "/" + EDITOR_FOLDER + "/" + TEMPLATES_FOLDER;
        public const string TEMPLATES_FOLDER = "Templates";
    }
}
