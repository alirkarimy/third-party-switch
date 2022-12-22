using UnityEngine;
using UnityEditor;
using System.Linq;

namespace Symlink.IO.Import
{
    [InitializeOnLoad]
    internal static class SourceImporterRemoverExample
    {
        private const string Example1ImportMenuPath = "Symlink/Importer/Target1";
        private const string Example1RemoveMenuPath = "Symlink/Remover/Target1";
        private const string Example2ImportMenuPath = "Symlink/Importer/Target2";
        private const string Example2RemoveMenuPath = "Symlink/Remover/Target2";


        private readonly static ExampleMenuItem[] _analyticsTargets = {
            new ExampleMenuItem(ExampleTarget.Target1 , Example1ImportMenuPath ) ,
            new ExampleMenuItem(ExampleTarget.Target2 , Example1RemoveMenuPath ),
            new ExampleMenuItem(ExampleTarget.Target1 , Example2ImportMenuPath ) ,
            new ExampleMenuItem(ExampleTarget.Target2 , Example2RemoveMenuPath )};


        readonly static string pathToProject ;
        readonly static string sourcePath ;
        readonly static string destinationPath ;


        static SourceImporterRemoverExample()
        {
            pathToProject = Application.dataPath;

            sourcePath = string.Concat(pathToProject.Split(new string[1] { "/Assets" }
                          , System.StringSplitOptions.None).FirstOrDefault() + "/ExampleSources");
            destinationPath = string.Concat(pathToProject, "/SymlinkUtility/");

            for (int i = 0; i < _analyticsTargets.Length; i++)
            {
                SetMenuItemCheckTo(_analyticsTargets[i].Path, IsMenuItemChecked(_analyticsTargets[i].Path));
            }

        }

        public static bool IsMenuItemChecked(string menuPath)
        {
            return EditorPrefs.GetBool(menuPath, false);
        }

        public static void SaveMenuItemCheckStatus(string menuPath, bool value)
        {
            EditorPrefs.SetBool(menuPath, value);
        }

        public static void SetMenuItemCheckTo(string menuPath, bool isChecked)
        {
            Menu.SetChecked(menuPath, isChecked);
        }


        [MenuItem(Example1ImportMenuPath)]
        private static void EnableExample1()
        {
            SetActiveMenu(Example1ImportMenuPath, true);
            SetActiveMenu(Example1RemoveMenuPath, false);
            SetSymbols(ExampleTarget.Target1);
        }

        [MenuItem(Example1RemoveMenuPath)]
        private static void DisableExample1()
        {
            SetActiveMenu(Example1RemoveMenuPath, true);
            SetActiveMenu(Example1ImportMenuPath, false);
            RemoveSymbols(ExampleTarget.Target1);
        }

        [MenuItem(Example2ImportMenuPath)]
        public static void EnableExample2()
        {
            SetActiveMenu(Example2ImportMenuPath, true);
            SetActiveMenu(Example2RemoveMenuPath, false);
            SetSymbols(ExampleTarget.Target2);
        }

        [MenuItem(Example2RemoveMenuPath)]
        public static void DisableExample2()
        {
            SetActiveMenu(Example2RemoveMenuPath, true);
            SetActiveMenu(Example2ImportMenuPath, false );
            RemoveSymbols(ExampleTarget.Target2);
        }


        private static void SetSymbols(ExampleTarget menuName)
        {            
            Importer.CreateSymlinkAddSymbol(sourcePath, destinationPath, menuName.ToString());
        }

        private static void RemoveSymbols(ExampleTarget menuName)
        {         
            Remover.RemoveSymlinkSymbol(destinationPath, menuName.ToString());
        }

        private static void SetActiveMenu(string menuPath,bool isActive)
        {
            ExampleMenuItem menuItem= null;
            FindMenuItem(menuPath, out menuItem);
            
            if (menuItem == null) Debug.LogError(string.Format("menuPath : {0} not found", menuPath));

            SaveMenuItemCheckStatus(menuItem.Path, isActive);
            SetMenuItemCheckTo(menuItem.Path, isActive);

        }
        private static void FindMenuItem(string menuPath,out ExampleMenuItem target)
        {
            target = _analyticsTargets.First(item => item.Path.CompareTo(menuPath) == 0) ;            
        }
        private static void FindMenuItem(ExampleTarget targetName, out ExampleMenuItem target)
        {
            target = _analyticsTargets.First(item => item.Name == targetName);

        }
    }

    public enum ExampleTarget
    {
        Target1,
        Target2
    }

    public class ExampleMenuItem : Symlink.MenuItem<ExampleTarget>
    {
        public ExampleMenuItem(ExampleTarget name, string path) : base(name, path)
        {
        }
    }
}