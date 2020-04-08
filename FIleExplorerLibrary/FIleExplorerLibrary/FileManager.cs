using FileSystem;
using System;
using System.Collections.Generic;

namespace FileExplorerLibrary
{
    public class FileManager
    {
        private List<BaseData> files;
        private string currentPath;


        public bool IsFileOpen { get; private set; }
        public bool IsCopy { get; set; }
        public bool IsFile { get; set; }


        private string clipboard;
        public string Clipboard {
            get 
            { 
                return clipboard; 
            }
            set
            {
                int dataIndex = Contains(currentPath + value);
                if (dataIndex != -1)
                {
                    if (files[dataIndex] is File)
                        IsFile = true;
                    else if (files[dataIndex] is Directory)
                        IsFile = false;
                    clipboard = currentPath + value;
                }
            }
        }




        public FileManager()
        {
            currentPath = @"C:\";
            IsFileOpen = false;
            IsFile = false;

            files = new List<BaseData>() { new Directory(@"C:\") };
            files = (files[0] as Directory).Open(currentPath);
        }




        public void Close()
        {
            IsFileOpen = false;
        }


        private int Contains(string path)
        {
            for (int i = 0; i < files.Count; i++)
                if (files[i].Path == path)
                    return i;
            return -1;
        }


        private void Copy()
        {
            if (IsFile)
            {
                File file = new File(currentPath);
                file.Copy(clipboard, currentPath + GetNameFromPath(clipboard));
            }
            else
                (files[0] as Directory).Copy(clipboard, currentPath + GetNameFromPath(clipboard));
        }


        public void Copy(string path)
        {
            Clipboard = path;
            IsCopy = true;
        }


        public void Create(string name)
        {

        }


        public void Cut(string path)
        {
            Clipboard = path;
            IsCopy = false;
        }

        public void Delete(string name)
        {

        }


        public List<BaseData> GetData()
        {
            return files;
        }


        private string GetNameFromPath(string path)
        {
            try
            {
                int separatorIndex = path.LastIndexOf(@"\") + 1; // doesn't count backslash in the end
                string name = path.Substring(separatorIndex);
                return name;
            }
            catch (Exception exception)
            {
                return "Couldn't get the name from path" + exception.Message;
            }
        }


        private void Move()
        {
            if (IsFile)
            {
                File file = new File(currentPath);
                file.Move(clipboard, currentPath + GetNameFromPath(clipboard));
            }
            else
                (files[0] as Directory).Move(clipboard, currentPath + GetNameFromPath(clipboard));
        }


        public void Open(string name)
        {
            if (name != @"..\")
            {
                int dataIndex = Contains(currentPath + name);
                if (dataIndex != -1)
                {
                    if (files[dataIndex] is Directory)
                    {
                        files = (files[dataIndex] as Directory).Open(currentPath + name);
                        currentPath = currentPath + name + @"\";
                    }
                    else if (files[dataIndex] is File)
                    {
                        (files[dataIndex] as File).Open(currentPath + name);
                        IsFileOpen = true;
                    }               
                }
            }
            else if (!currentPath.Equals(@"C:\"))
                GoToParentDirectory();
        }


        public void Paste()
        {
            if (!clipboard.Equals(""))
            {
                if (IsCopy)
                    Copy();
                else
                    Move();
            }
            RefreshList();
        }


        public void Rename()
        {

        }


        private void RefreshList()
        {
            clipboard = "";
            files.Clear();

            files = new List<BaseData>() { new Directory(currentPath) };
            files = (files[0] as Directory).Open(currentPath);
        }

        

        private void GoToParentDirectory()
        {
            currentPath = currentPath.Substring(0, currentPath.Length - 1); // doesn't count a backslash in the end
            int backslashIndex = currentPath.LastIndexOf(@"\");
            currentPath = currentPath.Substring(0, backslashIndex + 1); // keep a backslash in the end
            files = (files[0] as Directory).Open(currentPath);
        }
    }
}
