using FileExplorerLibrary;
using FileSystem;
using System;


namespace File_Explorer
{
    class File_Explorer
    {
        private const string title = "File Explorer";
        private static bool isAlive = true;
        private static FileManager fileManager = new FileManager();



        static void Main(string[] args)
        {
            WriteCenterColorText(title, ConsoleColor.Blue);
            printData();
            
            while (isAlive)
            {
                Console.WriteLine("Commands - Copy/Create/Delete/Open/Rename/Exit");
                string action = Console.ReadLine();

                switch (action)
                {
                    case "Copy":
                        break;
                    case "Create":
                        break;
                    case "Delete":
                        break;
                    case "Open":
                        Console.WriteLine("Write the name of the folder(file) which you want to open");
                        fileManager.Open(Console.ReadLine());
                        Console.WriteLine();
                        printData();
                        break;
                    case "Rename":
                        break;
                    case "Exit":
                        isAlive = false;
                        break;
                }
            }
        }



        private static void printData()
        {
            foreach (BaseData data in fileManager.GetData())
                Console.WriteLine(data.Path);
            Console.WriteLine();
        }

        private static void WriteCenterColorText(String text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(new string(' ', (Console.WindowWidth - text.Length) / 2)); 
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.Green;
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
