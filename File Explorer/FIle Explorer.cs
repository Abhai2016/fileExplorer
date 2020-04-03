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
                if (!fileManager.isFileOpen)
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
                            printData();
                            break;
                        case "Rename":
                            break;
                        case "Exit":
                            isAlive = false;
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("To close file write command \"Close\"");
                    if (Console.ReadLine().Equals("Close"))
                    {
                        fileManager.Close();
                        printData();
                    }
                }
            }
        }



        private static void printData()
        {
            Console.WriteLine();

            foreach (BaseData data in fileManager.GetData())
            {
                if (fileManager.isFileOpen)
                {
                    if ((data is File) && (data as File).Content.Length > 0)
                    {
                        Console.Write((data as File).Content);
                        break;
                    }
                }  
                else
                    Console.WriteLine(data.Path); 
            }  

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
