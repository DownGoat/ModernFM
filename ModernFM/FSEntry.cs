using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ModernFM
{
    class FSEntry
    {
        public string path;
        public FileInfo info;
        public FileAttributes attributes;

        public string Name { get; set; }
        public string DateModified { get; set; }
        public string DateCreated { get; set; }
        public string Size { get; set; }
        public string Type { get; set; }
        public bool DoubleDot { get; set; }

        public FSEntry(string path)
        {
            this.path = path;
        }

        public FSEntry(string path, bool load)
        {
            this.path = path;
            if (load)
            {
                this.load();
            }
        }

        public FSEntry(bool doubleDot, string path)
        {
            this.DoubleDot = doubleDot;
            Name = "..";
            DateCreated = DateModified = DateTime.Now.ToString("G");
            Size = "4096B";
            Type = "<Directory>";

            DirectoryInfo dinfo = new DirectoryInfo(path);
            try
            {
                path = dinfo.Parent.FullName;
            } catch (System.NullReferenceException)
            {
                // Redundant, but to specify that the path should not change if this exception occurs. Root has no parent folder.
                path = path;
            }
            
        }

        public bool IsDir()
        {
            return (attributes & FileAttributes.Directory) == FileAttributes.Directory;
        }

        public string FormatSize()
        {
            string str = "";

            if (IsDir())
            {
                return "";
            }

            if (info.Length / 1000 < 1)
            {
                str = String.Format("{0}B", info.Length);
            }

            else if (info.Length / 1000000 < 1)
            {
                str = String.Format("{0:F}K", ((float)info.Length) / 1000);
            }

            else if (info.Length / 1000000000 < 1)
            {
                str = String.Format("{0:F}M", ((float)info.Length) / 1000000);
            }

            else if (info.Length / 1000000000000 < 1)
            {
                str = String.Format("{0:F}G", ((float)info.Length) / 1000000000);
            }

            return str;
        }

        public string FormatType()
        {
            string str = "";

            if (IsDir())
            {
                str = "<Directory>";
            }

            else if (FileExtensions.IsExecutalbe(info.Extension))
            {
                str = "Executable";
            }

            else if (FileExtensions.IsArchive(info.Extension))
            {
                str = "Archive";
            }

            else if (FileExtensions.IsMedia(info.Extension))
            {
                str = "Media";
            }

            return str;
        }

        public void load()
        {
            info = new FileInfo(path);
            attributes = info.Attributes;

            Name = info.Name;
            DateCreated = String.Format("{0}", info.CreationTime);
            DateModified = String.Format("{0}", info.LastAccessTime);
            Size = FormatSize();
            Type = FormatType();
        }
    }
}
