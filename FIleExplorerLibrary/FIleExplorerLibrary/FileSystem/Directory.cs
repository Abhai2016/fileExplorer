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




        public override void Copy(string newPath)
        {
            DirectoryInfo sourceDirectory = new DirectoryInfo(Path);
            DirectoryInfo targetDirectory = new DirectoryInfo(newPath);

            if (sourceDirectory.Exists && !targetDirectory.Exists)
            {
                CopyAll(sourceDirectory, targetDirectory);
                SetEvent("Copied", $"Folder {GetNameFromPath(Path)} successfully copied to {targetDirectory.FullName}");
            }
            else if (!sourceDirectory.Exists)
                SetEvent("Copied", $"Folder {GetNameFromPath(Path)} doesnt exist in {Path}");
            else
                SetEvent("Copied", $"Folder {GetNameFromPath(Path)} already exists in {targetDirectory.FullName}");
        }


        private void CopyAll(DirectoryInfo sourceDirectory, DirectoryInfo targetDirectory)
        {
            System.IO.Directory.CreateDirectory(targetDirectory.FullName);

            foreach (FileInfo file in sourceDirectory.GetFiles())
                file.CopyTo(System.IO.Path.Combine(targetDirectory.FullName, file.Name), true);

            foreach (DirectoryInfo diSourceSubDir in sourceDirectory.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir = targetDirectory.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }  
        }


        public override void Create(string path)
        {
            try
            {
                System.IO.Directory.CreateDirectory(path);
                SetEvent("Created", $"Folder {GetNameFromPath(path)} successfully created");
            }
            catch
            {
                SetEvent("Created", $"Coudln't create a folder {GetNameFromPath(path)}");
            }
        }


        public override void Delete(string path)
        {
            try
            {
                System.IO.Directory.Delete(path, true);
                SetEvent("Deleted", $"Folder {GetNameFromPath(path)} successfully deleted");
            }
            catch
            {
                SetEvent("Deleted", $"Couldn't delete {GetNameFromPath(path)}");
            }
        }


        public override void Move(string newPath)
        {
            MoveTo("Moved", newPath, $"Folder {GetNameFromPath(Path)} successfully moved to {newPath}", $"Folder {GetNameFromPath(Path)} doesn't exist", $"Folder already exists in {newPath}");    
        }


        private void MoveTo(string fileManagerStateHandler, string newPath, string success, string notFound, string alreadyExists)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Path);
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


        public override void Rename(string newPath)
        {
            MoveTo("Renamed", newPath, $"Folder successfully reanmed", $"Folder {GetNameFromPath(Path)} doesn't exist", $"Folder with this name already exists");
        }
    }
}
