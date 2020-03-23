namespace FileSystem
{
    public class Directory
    {
/*
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

                SetEvent("Copied", $"Папка {getNameFromPath(oldPath)} успешна скопирована в {newPath}");
            }
            catch (Exception exception)
            {
                SetEvent("Copied", $"Папка {getNameFromPath(oldPath)} не найдена по пути {oldPath}. {exception.Message}");
            } 
        }


        public override void Create(string path)
        {
            try
            {
                System.IO.Directory.CreateDirectory(path);
                SetEvent("Created", $"Папка {getNameFromPath(path)} успешна создана");
            }
            catch (Exception exception)
            {
                SetEvent("Created", $"Не удалось создать папку {getNameFromPath(path)}. {exception.Message}");
            }
        }


        public override void Delete(string path)
        {
            try
            {
                System.IO.Directory.Delete(path, true);
                SetEvent("Deleted", $"Папка {getNameFromPath(path)} успешна удалена");
            }
            catch (Exception exception)
            {
                SetEvent("Deleted", $"Не удалось удалить папку {getNameFromPath(path)}. {exception.Message}");
            }
        }


        public override void Move(string oldPath, string newPath)
        {
            MoveTo("Moved", oldPath, newPath, $"Папка {getNameFromPath(oldPath)} успешна перемещена в {newPath}", $"Папка {getNameFromPath(oldPath)} не найдена", $"Папка с таким именем уже существует в {newPath}");    
        }


        private void MoveTo(string fileManagerStateHandler, string oldPath, string newPath, string success, string notFound, string alreadyExists)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(oldPath);
            if (directoryInfo.Exists && !System.IO.Directory.Exists(newPath))
            {
                directoryInfo.MoveTo(newPath);
                SetEvent(fileManagerStateHandler, success);
            }
            else if (!directoryInfo.Exists)
                SetEvent(fileManagerStateHandler, notFound);
            else if (System.IO.Directory.Exists(newPath))
                SetEvent(fileManagerStateHandler, alreadyExists);
        }


        public override void Rename(string oldPath, string newPath)
        {
            MoveTo("Renamed", oldPath, newPath, $"Папка успешна переименована", $"Папка {getNameFromPath(oldPath)} не найдена", $"Папка с таким именем уже существует");
        }
        */
    }
}
