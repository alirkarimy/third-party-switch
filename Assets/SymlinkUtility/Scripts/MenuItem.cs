
using Symlink;

namespace Symlink
{
    public class MenuItem<T> where T : System.Enum
    {
        public readonly T Name;
        public readonly string Path;
        public MenuItem(T name, string path)
        {
            Name = name;
            Path = path;
        }

    }

}