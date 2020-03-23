using System;
using System.IO;

namespace FileExplorerLibrary
{
    public class FileManager
    {
        private const int BytesPerGigabyte = 1073741824;

        public delegate void FileManagerStateHandler(string message);

        protected internal event FileManagerStateHandler Copied;
        protected internal event FileManagerStateHandler Created;
        protected internal event FileManagerStateHandler Deleted;
        protected internal event FileManagerStateHandler Moved;
        protected internal event FileManagerStateHandler Renamed;


        public FileManager(FileManagerStateHandler copied, FileManagerStateHandler created,
            FileManagerStateHandler deleted, FileManagerStateHandler moved, FileManagerStateHandler renamed)
       {
            Copied += copied;
            Created += created;
            Deleted += deleted;
            Moved += moved;
            Renamed += renamed;
        }


        public void printDrives()
        {
            double totalDriveSpace = 0;
            double freeDriveSpace = 0;

            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    totalDriveSpace = (double)drive.TotalSize / BytesPerGigabyte;
                    freeDriveSpace = (double)drive.AvailableFreeSpace / BytesPerGigabyte;

                    Console.WriteLine($"{drive.VolumeLabel} {drive.Name}");
                    Console.WriteLine("Свободно {0:0.00} Гб из {1:0.00} Гб", freeDriveSpace, totalDriveSpace);
                }
                Console.WriteLine();
            }
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
