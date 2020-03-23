using System;
using System.IO;

namespace FileSystem
{
    public abstract class FIleSystem
    {
        public abstract void Copy(string oldPath, string newPath);

        public abstract void Create(string path);

        public abstract void Delete(string path);

        public abstract void Move(string oldPath, string newPath);

        public abstract void Rename(string oldPath, string newPath);


        public string[] GetDirectories(string path)
        {
            try
            {
                string[] directories = System.IO.Directory.GetDirectories(path);
                string[] files = System.IO.Directory.GetFiles(path);
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
            try
            {
                int separatorIndex = path.LastIndexOf(@"\");
                string name = path.Substring(separatorIndex);
                return name;
            }
            catch (Exception exception)
            {
                return "Не удалось получить имя файла из пути" + exception.Message;
            }
        }


       
    }
}
