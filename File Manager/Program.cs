using System;
using System.IO;
using FileManagerLibrary;

namespace File_Manager
{
    class Program
    {
        private const string title = "File Manager";

        private static bool isAlive = true;



        static void Main(string[] args)
        {
            WriteCenterColorText(title, ConsoleColor.Blue);

            FileSystem fileSystem = new FileSystem(Copied, Created, Deleted, Moved, Renamed);
            DriveInfo[] drivesInfo = fileSystem.GetDrives();

            printDrives(drivesInfo);

            while (isAlive)
            {
                Console.WriteLine("Для выхода введите 0");
                string path = Console.ReadLine();

                if (path.Equals("0"))
                    isAlive = false;
                else
                {
                    string[] directories = fileSystem.GetDirectories(path);
                    for (int i = 0; i < directories.Length; i++)
                        Console.WriteLine(directories[i]);
                }
                
            }
        }


        private static void WriteCenterColorText(String text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(new string(' ', (Console.WindowWidth - text.Length) / 2)); 
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.Green;
        }


        private static void printDrives(DriveInfo[] drivesInfo)
        {
            double totalDriveSpace = 0;
            double freeDriveSpace = 0;

            foreach (DriveInfo drive in drivesInfo)
            {
                if (drive.IsReady)
                {
                    totalDriveSpace = (double)drive.TotalSize / 1000000000;
                    freeDriveSpace = (double)drive.AvailableFreeSpace / 1000000000;

                    Console.WriteLine($"{drive.VolumeLabel} {drive.Name}");
                    Console.WriteLine("Объем диска: {0:0.00} Гб", totalDriveSpace);
                    Console.WriteLine("Свободное пространство: {0:0.00} Гб", freeDriveSpace);
                }
                Console.WriteLine();
            }
        }



        private static void Copied(string message)
        {
            Console.WriteLine(message);
        }


        private static void Created(string message)
        {
            Console.WriteLine(message);
        }


        private static void Deleted(string message)
        {
            Console.WriteLine(message);
        }

        
        private static void Moved(string message)
        {
            Console.WriteLine(message);
        }


        private static void Renamed(string message)
        {
            Console.WriteLine(message);
        }
    }
}
