using FileExplorerLibrary;
using System;


namespace File_Explorer
{
    class File_Explorer
    {
        private const string title = "File Explorer";

        private static bool isAlive = true;



        static void Main(string[] args)
        {
            WriteCenterColorText(title, ConsoleColor.Blue);

            FileManager fileManager = new FileManager(Copied, Created, Deleted, Moved, Renamed);
            fileManager.printDrives();


            while (isAlive)
            {
                Console.WriteLine("Для копирования файла введите 1, для перемещения 2, удаления 3, переименования 4, а для выхода введите 0");
                string path = Console.ReadLine();

                //TODO switch for actions

                if (path.Equals("0"))
                    isAlive = false;
                else
                {
                    
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
