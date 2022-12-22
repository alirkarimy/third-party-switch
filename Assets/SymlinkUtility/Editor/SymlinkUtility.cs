using UnityEditor;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Diagnostics;

namespace Symlink
{
    /**
     *  Original source can be found on https://github.com/karl-/unity-symlink-utility
	 *	An editor utility for easily creating symlinks in your project.
	 *	 
	 *	draws a small indicator in the Project view for folders that are
	 *	symlinks.
	 */
    [InitializeOnLoad]
    public static class SymlinkUtility
    {
        private enum SymlinkType
        {
            Junction,
            Absolute
        }

        // FileAttributes that match a junction folder.
        const FileAttributes FOLDER_SYMLINK_ATTRIBS = FileAttributes.Directory | FileAttributes.ReparsePoint;

        // Style used to draw the symlink indicator in the project view.
        private static GUIStyle _symlinkMarkerStyle = null;
        private static GUIStyle symlinkMarkerStyle
        {
            get
            {
                if (_symlinkMarkerStyle == null)
                {
                    _symlinkMarkerStyle = new GUIStyle(EditorStyles.label);
                    _symlinkMarkerStyle.normal.textColor = new Color(.2f, .8f, .2f, .8f);
                    _symlinkMarkerStyle.alignment = TextAnchor.MiddleRight;
                }
                return _symlinkMarkerStyle;
            }
        }

        /**
		 *	Static constructor subscribes to projectWindowItemOnGUI delegate.
		 */
        static SymlinkUtility()
        {
            EditorApplication.projectWindowItemOnGUI += OnProjectWindowItemGUI;
        }

        /**
		 *	Draw a little indicator if folder is a symlink
		 */
        private static void OnProjectWindowItemGUI(string guid, Rect r)
        {
            try
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);

                if (!string.IsNullOrEmpty(path))
                {
                    FileAttributes attribs = File.GetAttributes(path);

                    if ((attribs & FOLDER_SYMLINK_ATTRIBS) == FOLDER_SYMLINK_ATTRIBS)
                        GUI.Label(r, "<=>", symlinkMarkerStyle);
                }
            }
            catch { }
        }

        /**
		 *	Add a menu item in the Assets/Create category to add symlinks to directories.
		 */
#if UNITY_EDITOR_WIN
        internal static void Junction(string sourcePath, string linkPath)
        {
            Symlink(SymlinkType.Junction, sourcePath, linkPath);
        }
        internal static void RemoveJunction(string linkPath)
        {
            RemoveSymlink(SymlinkType.Junction, linkPath);
        }
#endif

        internal static void SymlinkAbsolute(string sourcePath, string linkPath)
        {
            Symlink(SymlinkType.Absolute, sourcePath, linkPath);
        }
        internal static void RemoveSymlinkAbsolute(string linkPath)
        {
            RemoveSymlink(SymlinkType.Absolute, linkPath);
        }


        static void Symlink(SymlinkType linkType, string sourceFolderPath, string targetPath)
        {

            UnityEngine.Debug.Log($"source : {sourceFolderPath} , target : {targetPath}");
            // Cancelled dialog
            if (string.IsNullOrEmpty(sourceFolderPath))
                return;

            if (sourceFolderPath.Contains(Application.dataPath))
            {
                UnityEngine.Debug.LogWarning("Cannot create a symlink to folder in your project!");
                return;
            }

            string sourceFolderName = sourceFolderPath.Split(new char[] { '/', '\\' }).LastOrDefault();

            if (string.IsNullOrEmpty(sourceFolderName))
            {
                UnityEngine.Debug.LogWarning("Couldn't deduce the folder name?");
                return;
            }

            if (string.IsNullOrEmpty(targetPath))
                targetPath = "Assets";

            UnityEngine.Debug.Log($"targetPath : {targetPath}");           
            
            if (Directory.Exists(targetPath) || !Directory.Exists(sourceFolderPath))
            {
                UnityEngine.Debug.LogWarning(string.Format("SourceFolder not exists or target folder already exists at this location, aborting link.\n{0} -> {1}", sourceFolderPath, targetPath));
                return;
            }
           
#if UNITY_EDITOR_WIN
            string linkOption = linkType == SymlinkType.Junction ? "/J" : "/D";
            string command = string.Format("mklink {0} \"{1}\" \"{2}\"", linkOption, targetPath, sourceFolderPath);
            UnityEngine.Debug.Log($"command : {command}");
            ExecuteCmdCommand(command, linkType != SymlinkType.Junction); // Symlinks require admin privilege on windows, junctions do not.
#elif UNITY_EDITOR_OSX
            // For some reason, OSX doesn't want to create a symlink with quotes around the paths, so escape the spaces instead.
            sourcePath = sourcePath.Replace(" ", "\\ ");
            targetPath = targetPath.Replace(" ", "\\ ");
            string command = string.Format("ln -s {0} {1}", sourceFolderPath, targetPath);
            ExecuteBashCommand(command);
#elif UNITY_EDITOR_LINUX
            // Is Linux the same as OSX?
#endif

            //UnityEngine.Debug.Log(string.Format("Created symlink: {0} <=> {1}", targetPath, sourceFolderPath));

            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
        }

        static void RemoveSymlink(SymlinkType linkType, string linkFolderPath)
        {

            // Cancelled dialog
            if (string.IsNullOrEmpty(linkFolderPath))
                return;

            // Get path to Folder.
            string linkFolderName = linkFolderPath.Split(new char[] { '/', '\\' }, System.StringSplitOptions.None).LastOrDefault();


            if (string.IsNullOrEmpty(linkFolderName))
            {
                UnityEngine.Debug.LogWarning("Couldn't deduce the folder name?");
                return;
            }
            linkFolderName = linkFolderName.Replace("/", "");
                       
            if (!Directory.Exists(linkFolderPath))
            {
                UnityEngine.Debug.LogWarning(string.Format("Folder does not exists at this location, aborting remove.\n{0}", linkFolderPath));
                return;
            }

#if UNITY_EDITOR_WIN
            string command = string.Format("rd \"{0}\"", linkFolderPath);

            ExecuteCmdCommand(command, linkType != SymlinkType.Junction); // Symlinks require admin privilege on windows, junctions do not.

#elif UNITY_EDITOR_OSX
            // For some reason, OSX doesn't want to create a symlink with quotes around the paths, so escape the spaces instead.
            sourcePath = sourcePath.Replace(" ", "\\ ");
            targetPath = targetPath.Replace(" ", "\\ ");
            string command = string.Format("ln -s {0} {1}", sourcePath, targetPath);
            ExecuteBashCommand(command);
#elif UNITY_EDITOR_LINUX
            // Is Linux the same as OSX?
#endif

            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
        }

        static void ExecuteCmdCommand(string command, bool asAdmin)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "CMD.exe",
                Arguments = "/C " + command,
                UseShellExecute = asAdmin,
                RedirectStandardError = !asAdmin,
                CreateNoWindow = true,
            };
            if (asAdmin)
            {
                startInfo.Verb = "runas"; // Runs process in admin mode. See https://stackoverflow.com/questions/2532769/how-to-start-a-process-as-administrator-mode-in-c-sharp
            }
            var proc = new Process()
            {
                StartInfo = startInfo
            };

            using (proc)
            {
                proc.Start();
                proc.WaitForExit();

                if (!asAdmin && !proc.StandardError.EndOfStream)
                {
                    UnityEngine.Debug.LogError(proc.StandardError.ReadToEnd());
                }
            }
        }

        static void ExecuteBashCommand(string command)
        {
            command = command.Replace("\"", "\"\"");

            var proc = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = "-c \"" + command + "\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }
            };

            using (proc)
            {
                proc.Start();
                proc.WaitForExit();

                if (!proc.StandardError.EndOfStream)
                {
                    UnityEngine.Debug.LogError(proc.StandardError.ReadToEnd());
                }
            }
        }
    }
}