using System.IO;

namespace FileManagerLibrary
{
    public class Files : FileManagerLibary
    {
        public Files(FileManagerStateHandler copied, FileManagerStateHandler created,
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
            if (File.Exists(oldPath) && !File.Exists(newPath))
            {
                File.Copy(oldPath, newPath);
                SetEvent("Copied", "Файл успешно скопирован");
            }
            else if (File.Exists(newPath))
                SetEvent("Copied", $"Файл с таким именем уже существует в {newPath}");
            else if (!File.Exists(oldPath))
                SetEvent("Copied", $"Файл {getNameFromPath(oldPath)} не найден");
        }


        public override void Create(string fileName)
        {
            if (!File.Exists(fileName))
            {
                File.Create(fileName);
                SetEvent("Created", "Файл успешно создан");
            }   
            else
                SetEvent("Created", "Такой файл уже существует");
        }


        public override void Delete(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
                SetEvent("Deleted", $"Файл {path} успешно удален");
            }
            else
                SetEvent("Deleted", $"Файл {path} не найден");
        }


        public override void Move(string oldPath, string newPath)
        {
            MoveTo("Moved", oldPath, newPath, $"Файл {getNameFromPath(oldPath)} успешно перемещен в {newPath}", $"Файл {getNameFromPath(oldPath)} не найден", $"Файл {getNameFromPath(oldPath)} уже существует в {newPath}");
        }


        private void MoveTo(string fileManagerStateHandler, string oldPath, string newPath, string success, string notFound, string alreadyExists)
        {
            if (File.Exists(oldPath) && !File.Exists(newPath))
            {
                File.Move(oldPath, newPath);
                SetEvent("Moved", success);
            }
            else if (!File.Exists(oldPath))
                SetEvent(fileManagerStateHandler, notFound);
            else if (File.Exists(newPath))
                SetEvent(fileManagerStateHandler, alreadyExists);
        }


        public override void Rename(string oldPath, string newPath)
        {
            MoveTo("Renamed", oldPath, newPath, "Файл успешно создан", $"Файл {getNameFromPath(oldPath)} не найден", $"Файл с таким именем уже существует в {newPath}");
        }
    }
}
