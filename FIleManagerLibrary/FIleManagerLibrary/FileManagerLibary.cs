using System;
using System.IO;

namespace FileManagerLibrary
{
    public abstract class FileManagerLibary
    {
        public delegate void FileManagerStateHandler(string message);

        protected internal event FileManagerStateHandler Copied;
        protected internal event FileManagerStateHandler Created;
        protected internal event FileManagerStateHandler Deleted;
        protected internal event FileManagerStateHandler Moved;
        protected internal event FileManagerStateHandler Renamed;




        public abstract void Copy(string oldPath, string newPath);

        public abstract void Create(string path);

        public abstract void Delete(string path);

        public abstract void Move(string oldPath, string newPath);

        public abstract void Rename(string oldPath, string newPath);



        public DriveInfo[] GetDrives()
        {
            return DriveInfo.GetDrives();
        }


        public string[] GetDirectories(string path)
        {
            try
            {
                string[] directories = Directory.GetDirectories(path);
                string[] files = Directory.GetFiles(path);
                string[] all = new string[directories.Length + files.Length];
                int indexDifference = 0;

                for (int i = 0; i < all.Length; i++)
                    if (i < directories.Length)
                        all[i] = directories[i];
                    else
                    {
                        if (indexDifference == 0)
                            indexDifference = i;
                        all[i] = files[i - indexDifference];
                    }
                        
                return all;
            }
            catch (DirectoryNotFoundException exception)
            {
                string[] message = { $"Такая директория не существует. {exception.Message}" };
                return message;
            }
        }


        protected string getNameFromPath(string path)
        {
            int separatorIndex = path.LastIndexOf(@"\");
            string name = path.Substring(separatorIndex);

            if (name != null)
                return name;
            else
                throw new Exception("Не удалось получить имя из пути");
        }


        protected void SetEvent(string eventType, string message)
        {
            switch (eventType)
            {
                case "Copied":
                    Copied?.Invoke(message);
                    break;
                case "Created":
                    Created?.Invoke(message);
                    break;
                case "Deleted":
                    Deleted?.Invoke(message);
                    break;
                case "Moved":
                    Moved?.Invoke(message);
                    break;
                case "Renamed":
                    Renamed?.Invoke(message);
                    break;
                default:
                    throw new Exception("Такого события не существует");
            }
        }
    }
}
