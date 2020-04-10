using FileSystem;
using System.Collections.Generic;
using static FileSystem.BaseData;

namespace FileExplorerLibrary
{
    public class FileManager
    {
        private event FileManagerStateHandler Copied;
        private event FileManagerStateHandler Created;
        private event FileManagerStateHandler Deleted;
        private event FileManagerStateHandler Moved;
        private event FileManagerStateHandler Renamed;

        private List<BaseData> files;
        private BaseData clipboard;
        private string currentPath;


        public bool IsFileOpen { get; private set; }
        public bool IsCopy { get; set; }





        public FileManager(FileManagerStateHandler copied, FileManagerStateHandler created,
            FileManagerStateHandler deleted, FileManagerStateHandler moved, FileManagerStateHandler renamed)
        {
            Copied = copied;
            Created = created;
            Deleted = deleted;
            Moved = moved;
            Renamed = renamed;

            currentPath = @"C:\";
            IsFileOpen = false;
            clipboard = new Directory(@"C:\");
            files = new List<BaseData>() { clipboard };
            files = (files[0] as Directory).Open(currentPath);
            SetEventHandlers();
        }





        private void CheckForExisting(string path)
        {
            int dataIndex = Contains(path);
            if (dataIndex != -1)
                clipboard = files[dataIndex];
            else
                clipboard = new Directory(path);
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
            clipboard.Copy(currentPath + GetNameFromPath(clipboard.Path));
        }


        public void Copy(string name)
        {
            CheckForExisting(currentPath + name);
            IsCopy = true;
        }


        public void Create(string fileOrFolder, string name)
        {
            if (fileOrFolder.Equals("Folder"))
                files[0].Create(currentPath + name);
            else if (fileOrFolder.Equals("File"))
            {
                File file = new File(currentPath + name);
                file.SetEventHandlers(Copied, Created, Deleted, Moved, Renamed);
                file.Create(currentPath + name);
            }
            RefreshList();
        }


        public void Cut(string name)
        {
            CheckForExisting(currentPath + name);
            IsCopy = false;
        }

        public void Delete(string name)
        {
            int dataIndex = Contains(currentPath + name);
            if (dataIndex != -1)
            {
                files[dataIndex].Delete(currentPath + name);
                RefreshList();
            }
            else
                files[0].Delete(currentPath + name);
        }


        public List<BaseData> GetData()
        {
            return files;
        }


        private void GoToParentDirectory()
        {
            currentPath = currentPath.Substring(0, currentPath.Length - 1); // doesn't count a backslash in the end
            int backslashIndex = currentPath.LastIndexOf(@"\");
            currentPath = currentPath.Substring(0, backslashIndex + 1); // keep a backslash in the end
            files = (files[0] as Directory).Open(currentPath);
        }


        private void Move()
        {
            clipboard.Move(currentPath + GetNameFromPath(clipboard.Path));
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
                SetEventHandlers();
            }
            else if (!currentPath.Equals(@"C:\"))
            {
                GoToParentDirectory();
                SetEventHandlers();
            }
        }


        public void Paste()
        {
            if (IsCopy)
                Copy();
            else
                Move();
            RefreshList();
        }


        private void RefreshList()
        {
            files.Clear();

            files = new List<BaseData>() { new Directory(currentPath) };
            files = (files[0] as Directory).Open(currentPath);
            SetEventHandlers();
        }


        public void Rename(string oldName, string newName)
        {
            int dataIndex = Contains(currentPath + oldName);

            if (dataIndex != -1)
                files[dataIndex].Rename(currentPath + newName);
            else
            {
                Directory directory = new Directory(currentPath + oldName);
                directory.SetEventHandlers(Copied, Created, Deleted, Moved, Renamed);
                directory.Rename(newName);
            }

            RefreshList();
        }


        private void SetEventHandlers()
        {
            foreach (BaseData file in files)
                file.SetEventHandlers(Copied, Created, Deleted, Moved, Renamed);
        }
    }
}
