using FileSystem;
using System.Collections.Generic;
using System.IO;

namespace FileExplorerLibrary
{
    public class FileManager
    {
        private List<BaseData> files;
        private string currentPath;

        public bool isFileOpen { get; private set; }




        public FileManager()
        {
            currentPath = @"C:\\";
            isFileOpen = false;

            files = new List<BaseData>() { new FileSystem.Directory(@"C:\\") };
            files = (files[0] as FileSystem.Directory).Open(currentPath);
        }




        public void Close()
        {
            isFileOpen = false;
        }


        private int Contains(string path)
        {
            for (int i = 0; i < files.Count; i++)
                if (files[i].Path == path)
                    return i;
            return -1;
        }


        public void Copy(string name)
        {

        }


        public void Create(string name)
        {

        }


        public void Delete(string name)
        {

        }


        public List<BaseData> GetData()
        {
            return files;
        }


        public void Move(string name)
        {

        }


        public void Open(string name)
        {
            if (name != @"..\")
            {
                int dataIndex = Contains(currentPath + name);
                if (dataIndex != -1)
                {
                    if (files[dataIndex] is FileSystem.Directory)
                    {
                        files = (files[dataIndex] as FileSystem.Directory).Open(currentPath + name);
                        currentPath = currentPath + name + @"\";
                    }
                    else if (files[dataIndex] is FileSystem.File)
                    {
                        (files[dataIndex] as FileSystem.File).Open(currentPath + name);
                        isFileOpen = true;
                    }               
                }
            }
            else if (!currentPath.Equals(@"C:\\"))
                GoToParentDirectory();
        }


        public void Rename()
        {

        }

        

        private void GoToParentDirectory()
        {
            currentPath = currentPath.Substring(0, currentPath.Length - 1); // doesn't count a backslash in the end
            int backslashIndex = currentPath.LastIndexOf(@"\");
            currentPath = currentPath.Substring(0, backslashIndex + 1); // keep a backslash in the end
            files = (files[0] as FileSystem.Directory).Open(currentPath);
        }
    }
}
