using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ModernFM
{
    class Context
    {
        public string Path { get; set; }
        public Stack<string> backwards = new Stack<string>();
        public Stack<string> forwards = new Stack<string>();



        public Context(string path)
        {
            this.Path = path;
        }

        public List<FSEntry> BrowseCurrentContext()
        {
            string[] files;


            files = Directory.GetFileSystemEntries(Path);


            List<FSEntry> file_list = new List<FSEntry>();
            List<FSEntry> dir_list = new List<FSEntry>();
            foreach (string name in files)
            {
                FSEntry f = new FSEntry(name, true);

                if (f.IsDir())
                {
                    dir_list.Add(f);
                }

                else
                {
                    file_list.Add(f);
                }
            }

            var retVal = new List<FSEntry>(dir_list.Count + file_list.Count);
            retVal.AddRange(dir_list);
            retVal.AddRange(file_list);
            return retVal;
        }

        public void ChangeContext(string path)
        {
            backwards.Push(Path);
            forwards.Clear();
            this.Path = path;
        }

        public bool Back()
        {
            if (backwards.Count != 0)
            {
                forwards.Push(Path);
                Path = backwards.Pop();

                return true;
            }

            return false;
        }

        public bool Forward()
        {
            if (forwards.Count != 0)
            {
                backwards.Push(Path);
                Path = forwards.Pop();

                return true;
            }

            return false;
        }

        internal void ChangeContext(string path, string name)
        {
            string newPath = System.IO.Path.Combine(path, name);
            ChangeContext(newPath);
        }

        public void Parent()
        {
            DirectoryInfo dinfo = new DirectoryInfo(Path);
            try
            {
                ChangeContext(dinfo.Parent.FullName);
            } catch(Exception e)
            {

            }
        }
    }
}
