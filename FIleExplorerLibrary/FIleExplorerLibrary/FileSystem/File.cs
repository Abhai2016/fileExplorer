using System;
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



        public override void Copy(string newPath)
        {
            try 
            {
                System.IO.File.Copy(Path, newPath);
                SetEvent("Copied", "File successfuly copied");
            }
            catch (Exception exception)
            {
                if (System.IO.File.Exists(newPath))
                    SetEvent("Copied", $"File already exists in {newPath}");
                else if (!System.IO.File.Exists(Path))
                    SetEvent("Copied", $"File {GetNameFromPath(Path)} not found");
                else
                    SetEvent("Copied", exception.Message);
            }    
        }


        public override void Create(string fileName)
        {
            if (!System.IO.File.Exists(fileName))
            {
                System.IO.File.Create(fileName);
                SetEvent("Created", "File successfuly created");
            }   
            else
                SetEvent("Created", "File already exists here");
        }


        public override void Delete(string path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                SetEvent("Deleted", $"File {GetNameFromPath(path)} successfuly deleted");
            }
            else
                SetEvent("Deleted", $"File {GetNameFromPath(path)} not found");
        }


        public override void Move(string newPath)
        {
            MoveTo("Moved", Path, newPath, $"File {GetNameFromPath(Path)} successfuly moved in {newPath}", $"File {GetNameFromPath(Path)} not foudn", $"File {GetNameFromPath(Path)} already exists in {newPath}");
        }


        private void MoveTo(string fileManagerStateHandler, string oldPath, string newPath, string success, string notFound, string alreadyExists)
        {
            try
            {
                System.IO.File.Move(oldPath, newPath);
                SetEvent("Moved", success);
            }
            catch (Exception exception)
            {
                if (!System.IO.File.Exists(oldPath))
                    SetEvent(fileManagerStateHandler, notFound);
                else if (System.IO.File.Exists(newPath))
                    SetEvent(fileManagerStateHandler, alreadyExists);
                else
                    SetEvent(fileManagerStateHandler, exception.Message);
            }
        }


        public void Open(string path)
        {
            using (StreamReader streamReader = new StreamReader(path))
            {
                while (!streamReader.EndOfStream)
                    Content.AppendLine(streamReader.ReadLine());
            }
        }


        public override void Rename(string newPath)
        {
            MoveTo("Renamed", Path, newPath, "File successfuly moved", $"File {GetNameFromPath(Path)} not found", $"File already exists in {newPath}");
        }
    }
}
