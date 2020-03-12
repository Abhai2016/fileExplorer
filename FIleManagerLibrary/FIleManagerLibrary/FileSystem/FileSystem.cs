using System;
using System.IO;

namespace FIleManagerLibrary.FileSystem
{
    abstract class FileSystem
    {
        protected internal delegate void FileManagerStateHandler(string message);

        protected internal event FileManagerStateHandler Copied;
        protected internal event FileManagerStateHandler Created;
        protected internal event FileManagerStateHandler Deleted;
        protected internal event FileManagerStateHandler Moved;
        protected internal event FileManagerStateHandler Renamed;




        public abstract void Copy(string oldPath, string newPath);

        public abstract void Create(string path);

        public abstract void Delete(string path);

        public abstract void Move(string oldPath, string newPath);

        public abstract void Rename(string oldName, string newName);



        public DriveInfo[] GetDrives()
        {
            return DriveInfo.GetDrives();
        }


        public string[] GetDirectories(string path)
        {
            string[] directories = Directory.GetDirectories(path);
            string[] files = Directory.GetFiles(path);
            string[] all = new string[directories.Length + files.Length];

            for (int i = 0; i < all.Length; i++)
                if (i < directories.Length)
                    all[i] = directories[i];
                else
                    all[i] = files[i];

            return all;
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
