using System;
using System.IO;
using System.Collections.Generic;
class main
{
    static List<command> dataBase = new List<command>() {};
    static string[] types = new string[] { "string ' '", "int ` `" };
    public static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = ConsoleColor.DarkMagenta;
        Console.Clear();
        Console.WriteLine("******************************************************");
        Console.WriteLine("***                                                ***");
        Console.WriteLine("***    Hello and welcome to the Galak Terminal!    ***");
        Console.WriteLine("***    Press Ctrl + C to exit or type 'help'!      ***");
        Console.WriteLine("***                                                ***");
        Console.WriteLine("******************************************************");
        while (true)
        {
            Console.Write(">");
            interpret(Console.ReadLine());
        }
    }
    public static void interpret(string line) {
        switch (line) {
            case "help":
                Console.Clear();
                Console.WriteLine("********************************************************");
                Console.WriteLine("***                                                  ***");
                Console.WriteLine("***     Welcome! To create a new command, type:      ***");
                Console.WriteLine("***             createCommand [filePath]             ***");
                Console.WriteLine("*** Where filePath links to a file with your command ***");
                Console.WriteLine("***                                                  ***");
                Console.WriteLine("***  Type 'help 1' for how to create a command file  ***");
                Console.WriteLine("***                      1/2                         ***");
                Console.WriteLine("***                                                  ***");
                Console.WriteLine("********************************************************");
                break;
            case "clear":
                Console.Clear();
                break;
        }
    }
    public static string identifyType(string word) {
        foreach (string s in types) {
            string req = s.Substring(s.IndexOf(' '));
            if (matchesReqs(word, req)) {
                return s.Substring(0, s.IndexOf(' '));
            }
        }
        return "undefined";
    }
    public static bool matchesReqs(string s, string reqs) {
        return true;
    }
}
class command
{
    public command(string name) {

	} 
}
