using System;
using System.IO;

namespace File_Manager
{
    class Program
    {
        private const string title = "File Manager";



        static void Main(string[] args)
        {
            WriteCenterColorText(title, ConsoleColor.Blue);
        }


        private static void WriteCenterColorText(String text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(new string(' ', (Console.WindowWidth - text.Length) / 2)); 
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.Green;
        }
    }
}
