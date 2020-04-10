using FileExplorerLibrary;
using FileSystem;
using System;


namespace File_Explorer
{
    class File_Explorer
    {
        private const string title = "File Explorer";
        private const string fileOrDirectroryName = "Write the name of the folder(file) which you want to ";
        private static bool isAlive = true;

        private static FileManager fileManager = new FileManager(Copied, Created, Deleted, Moved, Renamed);



        static void Main(string[] args)
        {
            WriteCenterColorText(title, ConsoleColor.Blue);
            printData();
            
            while (isAlive)
            {
                if (!fileManager.IsFileOpen)
                {
                    Console.WriteLine("Commands - Open/Cut/Copy/Create/Delete/Rename/Exit");
                    string action = Console.ReadLine();

                    switch (action)
                    {
                        case "Open":
                            Console.WriteLine(fileOrDirectroryName + "open");
                            fileManager.Open(Console.ReadLine());
                            break;
                        case "Cut":
                            Console.WriteLine(fileOrDirectroryName + "move");
                            fileManager.Cut(Console.ReadLine());
                            break;
                        case "Copy":
                            Console.WriteLine(fileOrDirectroryName + "copy");
                            fileManager.Copy(Console.ReadLine());
                            break;
                        case "Paste":
                            fileManager.Paste();
                            break;
                        case "Create":
                            break;
                        case "Delete":
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
                    }
                }
                printData();
            }
        }



        private static void printData()
        {
            Console.WriteLine();

            foreach (BaseData data in fileManager.GetData())
            {
                if (fileManager.IsFileOpen)
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
