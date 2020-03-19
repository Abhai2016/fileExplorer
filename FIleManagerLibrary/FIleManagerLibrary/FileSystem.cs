using System;
using System.IO;

namespace FileManagerLibrary
{
    public class FileSystem : FileManagerLibary
    {
        public FileSystem(FileManagerStateHandler copied, FileManagerStateHandler created,
            FileManagerStateHandler deleted, FileManagerStateHandler moved, FileManagerStateHandler renamed)
        {
            Copied += copied;
            Created += created;
            Deleted += deleted;
            Moved += moved;
            Renamed += renamed;
        }


        

        public override void Copy(string oldPath, string newPath)
        {
            foreach (string dirPath in Directory.GetDirectories(oldPath, "*",
                SearchOption.AllDirectories))
                if (!Directory.Exists(newPath))
                    Directory.CreateDirectory(dirPath.Replace(oldPath, newPath));

            foreach (string dirPath in Directory.GetFiles(oldPath, "*.*",
                SearchOption.AllDirectories))
                if (!File.Exists(newPath))
                    File.Copy(dirPath, dirPath.Replace(oldPath, newPath), true);

            SetEvent("Copied", $"Папка {getNameFromPath(oldPath)} успешна скопирована в {newPath}");
        }


        public override void Create(string path)
        {
            Directory.CreateDirectory(path);
            SetEvent("Created", $"Папка {getNameFromPath(path)} успешна создана");
        }


        public override void Delete(string path)
        {
            try
            {
                Directory.Delete(path, true);
                SetEvent("Deleted", $"Папка {getNameFromPath(path)} успешна удалена");
            }
            catch (Exception exception)
            {
                SetEvent("Deleted", $"Не удалось удалить папку {getNameFromPath(path)}. " + exception.Message);
            }
        }


        public override void Move(string oldPath, string newPath)
        {
            MoveTo("Moved", oldPath, newPath, $"Папка {getNameFromPath(oldPath)} успешна перемещена в {newPath}", $"Папка {getNameFromPath(oldPath)} не найдена", $"Папка с таким именем уже существует в {newPath}");    
        }


        private void MoveTo(string fileManagerStateHandler, string oldPath, string newPath, string success, string notFound, string alreadyExists)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(oldPath);
            if (directoryInfo.Exists && !Directory.Exists(newPath))
            {
                directoryInfo.MoveTo(newPath);
                SetEvent(fileManagerStateHandler, success);
            }
            else if (!directoryInfo.Exists)
                SetEvent(fileManagerStateHandler, notFound);
            else if (Directory.Exists(newPath))
                SetEvent(fileManagerStateHandler, alreadyExists);
        }


        public override void Rename(string oldPath, string newPath)
        {
            MoveTo("Renamed", oldPath, newPath, $"Папка успешна переименована", $"Папка {getNameFromPath(oldPath)} не найдена", $"Папка с таким именем уже существует");
        }
    }
}
