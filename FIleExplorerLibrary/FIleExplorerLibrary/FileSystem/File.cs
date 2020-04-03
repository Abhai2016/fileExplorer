using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileSystem
{
    public class File : BaseData
    {
        public StringBuilder Content { get; set; }



        public File(string path)
        {
            Path = path;
            Content = new StringBuilder();
        }



        public override void Copy(string oldPath, string newPath)
        {
            if (System.IO.File.Exists(oldPath) && !System.IO.File.Exists(newPath))
            {
                System.IO.File.Copy(oldPath, newPath);
                SetEvent("Copied", "Файл успешно скопирован");
            }
            else if (System.IO.File.Exists(newPath))
                SetEvent("Copied", $"Файл с таким именем уже существует в {newPath}");
            else if (!System.IO.File.Exists(oldPath))
                SetEvent("Copied", $"Файл {getNameFromPath(oldPath)} не найден");
        }


        public override void Create(string fileName)
        {
            if (!System.IO.File.Exists(fileName))
            {
                System.IO.File.Create(fileName);
                SetEvent("Created", "Файл успешно создан");
            }   
            else
                SetEvent("Created", "Такой файл уже существует");
        }


        public override void Delete(string path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
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
            if (System.IO.File.Exists(oldPath) && !System.IO.File.Exists(newPath))
            {
                System.IO.File.Move(oldPath, newPath);
                SetEvent("Moved", success);
            }
            else if (!System.IO.File.Exists(oldPath))
                SetEvent(fileManagerStateHandler, notFound);
            else if (System.IO.File.Exists(newPath))
                SetEvent(fileManagerStateHandler, alreadyExists);
        }


        public void Open(string path)
        {
            using (StreamReader streamReader = new StreamReader(path))
            {
                while (!streamReader.EndOfStream)
                    Content.AppendLine(streamReader.ReadLine());
            }
        }


        public override void Rename(string oldPath, string newPath)
        {
            MoveTo("Renamed", oldPath, newPath, "Файл успешно создан", $"Файл {getNameFromPath(oldPath)} не найден", $"Файл с таким именем уже существует в {newPath}");
        }
    }
}
