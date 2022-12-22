using UnityEditor;

namespace Symlink.IO
{
    [InitializeOnLoad]
    public static class Importer
    {
        public static void CreateSymlinkAddSymbol(string sourcePath, string destinationPath,string target)
        {
            SymlinkUtility.SymlinkAbsolute(string.Concat(sourcePath,'/',target), string.Concat(destinationPath, target));
            PlayerSettingsWrapper.AddSymbolToPlatform(BuildTargetGroup.Android, target);
        }
    }

}

