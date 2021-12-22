using UnityEditor;
namespace Swither.Analytics
{
    public static class PlayerSettingsWrapper
    {
        public static void AddSymbolToPlatform(BuildTargetGroup targetGroup, string symbol)
        {
            string symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroup);

            if (!symbols.Contains(symbol))
            {
                symbols = string.Concat(symbols, $";{symbol}");
            }
            PlayerSettings.SetScriptingDefineSymbolsForGroup(targetGroup, symbols);
        }
        public static void RemoveSymbolFromPlatform(BuildTargetGroup targetGroup, string symbol)
        {
            string symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroup);
            if (symbols.Contains(symbols))
            {
                symbols = symbols.Replace(symbol, "");
            }
            PlayerSettings.SetScriptingDefineSymbolsForGroup(targetGroup, symbols);
        }
    }
}