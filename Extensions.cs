using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HACGUI.Extensions
{
    public static class Extensions
    {

        public static FileInfo GetFile(this DirectoryInfo obj, string filename) => new FileInfo($"{obj.FullName}{Path.DirectorySeparatorChar}{filename}");

        public static bool ContainsFile(this DirectoryInfo obj, string filename) => obj.GetFile(filename).Exists;

        public static DirectoryInfo GetDirectory(this DirectoryInfo obj, string foldername) => new DirectoryInfo($"{obj.FullName}{Path.DirectorySeparatorChar}{foldername.Replace('/', Path.DirectorySeparatorChar)}");

        public static FileInfo FindFile(this DirectoryInfo root, string filename, SearchOption option = SearchOption.AllDirectories) => root.FindFiles(new string[] { filename }, option)[0];

        public static FileInfo[] FindFiles(this DirectoryInfo root, string[] filenames, SearchOption option = SearchOption.AllDirectories)
        {
            FileInfo[] infos = new FileInfo[filenames.Length];
            foreach (FileInfo file in root.EnumerateFiles("*", option))
            {
                foreach (string filename in filenames)
                {
                    void Check()
                    {
                        if (file.Name == filename)
                        {
                            infos[Array.IndexOf(filenames, filename)] = file;
                            return;
                        }
                    };
                    Check(); // awful hack to prevent a break from exiting the entire loop
                }
            }
            return infos;
        }

        public static void DeleteRecursively(this DirectoryInfo obj)
        {
            foreach (FileInfo file in obj.EnumerateFiles())
                file.Delete();

            foreach (DirectoryInfo directory in obj.EnumerateDirectories("*", SearchOption.AllDirectories))
            {
                foreach (FileInfo file in directory.EnumerateFiles())
                    file.Delete();
                directory.Delete();
            }

            obj.Delete();
        }

        //thanks ms
        public static void DirectoryCopy(this DirectoryInfo Source, string Dest, bool Overwrite)
        {
            DirectoryInfo[] dirs = Source.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(Dest))
            {
                Directory.CreateDirectory(Dest);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = Source.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(Dest, file.Name);
                file.CopyTo(temppath, Overwrite);
            }

            foreach (DirectoryInfo subdir in dirs)
            {
                string temppath = Path.Combine(Dest, subdir.Name);
                DirectoryCopy(subdir, temppath, Overwrite);
            }
        }

        public static IEnumerable<T> Without<T>(this IEnumerable<T> a, IEnumerable<T> b) => a.Where(i => !b.Contains(i));

        public static string ReadAllText(this FileInfo file) => File.ReadAllText(file.FullName);

        public static string GetFileNameWithoutExtension(this FileInfo file) => file.Name.Replace(file.Extension, "");

        public static void WriteAllText(this FileInfo file, string contents) => File.WriteAllText(file.FullName, contents);
    }
}
