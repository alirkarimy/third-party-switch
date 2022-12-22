using UnityEditor;

namespace Symlink.IO
{
    [InitializeOnLoad]
    public static class Remover
    {
        public static void RemoveSymlinkSymbol(string destinationPath,string target)
        {
            SymlinkUtility.RemoveSymlinkAbsolute(string.Concat(destinationPath, target));
            PlayerSettingsWrapper.RemoveSymbolFromPlatform(BuildTargetGroup.Android, target);
        }
    }

}

