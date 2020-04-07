using System.Collections.Generic;
using System.IO;

namespace FileSystem
{
    public class Directory : BaseData 
    {

        public Directory(string path)
        {
            Path = path;
        }


        public override void Copy(string oldPath, string newPath)
        {
            try
            {
                foreach (string dirPath in System.IO.Directory.GetDirectories(oldPath, "*",
                SearchOption.AllDirectories))
                    if (!System.IO.Directory.Exists(newPath))
                        System.IO.Directory.CreateDirectory(dirPath.Replace(oldPath, newPath));

                foreach (string dirPath in System.IO.Directory.GetFiles(oldPath, "*.*",
                    SearchOption.AllDirectories))
                    if (!System.IO.File.Exists(newPath))
                        System.IO.File.Copy(dirPath, dirPath.Replace(oldPath, newPath), true);

                SetEvent("Copied", $"Folder {getNameFromPath(oldPath)} successfully copied to {newPath}");
            }
            catch
            {
                SetEvent("Copied", $"Folder {getNameFromPath(oldPath)} doesnt exist in {oldPath}");
            } 
        }


        public override void Create(string path)
        {
            try
            {
                System.IO.Directory.CreateDirectory(path);
                SetEvent("Created", $"Folder {getNameFromPath(path)} successfully created");
            }
            catch
            {
                SetEvent("Created", $"Coudln't create a folder {getNameFromPath(path)}");
            }
        }


        public override void Delete(string path)
        {
            try
            {
                System.IO.Directory.Delete(path, true);
                SetEvent("Deleted", $"Folder {getNameFromPath(path)} successfully deleted");
            }
            catch
            {
                SetEvent("Deleted", $"Couldn't delete {getNameFromPath(path)}");
            }
        }


        public override void Move(string oldPath, string newPath)
        {
            MoveTo("Moved", oldPath, newPath, $"Folder {getNameFromPath(oldPath)} successfully moved to {newPath}", $"Folder {getNameFromPath(oldPath)} doesn't exist", $"Folder already exists in {newPath}");    
        }


        private void MoveTo(string fileManagerStateHandler, string oldPath, string newPath, string success, string notFound, string alreadyExists)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(oldPath);
            if (directoryInfo.Exists && System.IO.Directory.Exists(newPath) == false)
            {
                directoryInfo.MoveTo(newPath);
                SetEvent(fileManagerStateHandler, success);
            }
            else if (!directoryInfo.Exists)
                SetEvent(fileManagerStateHandler, notFound);
            else if (System.IO.Directory.Exists(newPath))
                SetEvent(fileManagerStateHandler, alreadyExists);
        }


        public List<BaseData> Open(string path)
        {
            string[] stringDirectories = System.IO.Directory.GetDirectories(path);
            string[] stringFiles = System.IO.Directory.GetFiles(path);
            List<BaseData> listDirectoriesAndFiles = new List<BaseData>() { new Directory(@"..\") };

            for (int i = 0; i < stringDirectories.Length; i++)
                listDirectoriesAndFiles.Add(new Directory(stringDirectories[i]));
            for (int i = 0; i < stringFiles.Length; i++)
                listDirectoriesAndFiles.Add(new File(stringFiles[i]));

            return listDirectoriesAndFiles;
        }


        public override void Rename(string oldPath, string newPath)
        {
            MoveTo("Renamed", oldPath, newPath, $"Folder successfully reanmed", $"Folder {getNameFromPath(oldPath)} doesn't exist", $"Folder with this name already exists");
        }
    }
}
