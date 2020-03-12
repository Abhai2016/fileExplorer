using System.IO;

namespace FIleManagerLibrary.FileSystem
{
    class Files : FileSystem
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
                SetEvent("Copied", $"Файл {oldPath} не найден");
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


        public override void Delete(string fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
                SetEvent("Deleted", $"Файл {fileName} успешно удален");
            }
            else
                SetEvent("Deleted", $"Файл {fileName} не найден");
        }


        public override void Move(string oldPath, string newPath)
        {
            MoveTo("Moved", oldPath, newPath, $"Файл {oldPath} успешно перемещен в {newPath}", $"Файл {oldPath} не найден", $"Файл {oldPath} уже существует в {newPath}");
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


        public override void Rename(string oldName, string newName)
        {
            MoveTo("Renamed", oldName, newName, "Файл успешно создан", $"Файл {oldName} не найден", $"Файл с таким именем уже существует в {newName}");
        }
    }
}
