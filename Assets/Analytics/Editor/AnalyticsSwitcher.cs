using UnityEngine;
using UnityEditor;

namespace Swither.Analytics
{
    [InitializeOnLoad]
    internal static class AnalyticsSwitcher
    {
        private const string AdtraceMenuPath = "Window/Switcher/Analytics/AdTrace";
        private const string WebengageMenuPath = "Window/Switcher/Analytics/WebEngage";

        private readonly static AnalyticsMenuItem[] _analyticsTargets = {
            new AnalyticsMenuItem(AnalyticsTarget.AdTrace , AdtraceMenuPath ) ,
            new AnalyticsMenuItem(AnalyticsTarget.WebEngage , WebengageMenuPath )};

        static AnalyticsSwitcher()
        {
            string symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
            for (int i = 0; i < _analyticsTargets.Length; i++)
            {
                SetMenuCheck(_analyticsTargets[i].Path, GetMenuStatus(_analyticsTargets[i].Name));
            }

        }

        public static bool GetMenuStatus(AnalyticsTarget key)
        {
            return EditorPrefs.GetBool(key.ToString(), false);
        }

        public static void SetMenuStatus(AnalyticsTarget key, bool value)
        {
            EditorPrefs.SetBool(key.ToString(), value);
        }

        public static void SetMenuCheck(string menuPath, bool isChecked)
        {
            Menu.SetChecked(menuPath, isChecked);
        }


        [MenuItem(AdtraceMenuPath)]
        private static void EnableAdtrace()
        {
            SetActiveMenu(AnalyticsTarget.AdTrace);
            SetSymbols(AnalyticsTarget.AdTrace);

        }

        [MenuItem(WebengageMenuPath)]
        public static void EnableWebengage()
        {
            SetActiveMenu(AnalyticsTarget.WebEngage);
            SetSymbols(AnalyticsTarget.WebEngage);
        }


        private static void SetSymbols(AnalyticsTarget menuName)
        {
            string pathToProject = Application.dataPath;

            for (int i = 0; i < _analyticsTargets.Length; i++)
            {
                PlayerSettingsWrapper.RemoveSymbolFromPlatform(BuildTargetGroup.Android, _analyticsTargets[i].Name.ToString());
                SymlinkUtility.RemoveSymlinkAbsolute(string.Concat(pathToProject, "/Analytics/", _analyticsTargets[i].Name.ToString()));
            }

            SymlinkUtility.SymlinkAbsolute(string.Concat(pathToProject.Split(new string[1] { "/Assets" }, System.StringSplitOptions.None)[0] + "/AnalyticsPackages", "/", menuName.ToString()), string.Concat(pathToProject, "/Analytics/", menuName.ToString()));
            PlayerSettingsWrapper.AddSymbolToPlatform(BuildTargetGroup.Android, menuName.ToString());

        }

        private static void SetActiveMenu(AnalyticsTarget menuName)
        {
            for (int i = 0; i < _analyticsTargets.Length; i++)
            {
                SetMenuStatus(_analyticsTargets[i].Name, _analyticsTargets[i].Name.Equals(menuName));
                SetMenuCheck(_analyticsTargets[i].Path, _analyticsTargets[i].Name.Equals(menuName));
            }
        }

    }

    internal enum AnalyticsTarget
    {
        WebEngage,
        AdTrace
    }

    class AnalyticsMenuItem
    {
        internal readonly AnalyticsTarget Name;
        internal readonly string Path;
        public AnalyticsMenuItem(AnalyticsTarget name, string path)
        {
            Name = name;
            Path = path;
        }

    }
}