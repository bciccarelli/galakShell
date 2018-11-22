using System;
using System.IO;
using System.Collections.Generic;
#pragma warning disable 0649
class main
{
    static string dataFileName = "data.gshell";
    static string[] types = new string[] { "string ' '", "int ` `" };
    static List<string> paths = new List<string>();
    static List<commandFile> commandFiles = new List<commandFile>();
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

        setup();
        foreach (commandFile c in commandFiles)
        {
            Console.WriteLine(c.commands[0].parameters[1]);
        }
        while (true)
        {
            Console.Write(">");
            interpret(Console.ReadLine());
        }
    }
    public static string[] readFile(string fileName) {
        return File.ReadAllLines(fileName);
    }
    public static void setup()
    {
        foreach (string s in readFile(dataFileName)) {
            paths.Add(s);
        }
        foreach (string p in paths)
        {
            commandFiles.Add(new commandFile(readFile(p)));
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
            case "help 1":
                Console.Clear();
                Console.WriteLine("********************************************************");
                Console.WriteLine("***                                                  ***");
                Console.WriteLine("***    To create a commandfile, use the .gshell      ***");
                Console.WriteLine("***   file extension. Take a look at the example     ***");
                Console.WriteLine("***       file and the galak-c documentation.        ***");
                Console.WriteLine("***                                                  ***");
                Console.WriteLine("***                      2/2                         ***");
                Console.WriteLine("***                                                  ***");
                Console.WriteLine("********************************************************");
                break;
            case "clear":
                Console.Clear();
                break;
        }
        identifyCommand(line);
    }
    public static void identifyCommand(string s) {
        foreach (commandFile cf in commandFiles) {
            foreach (command c in cf.commands)
            {
                if (s.IndexOf(' ') > 0)
                {
                    if (c.name == s.Substring(0, s.IndexOf(' ')))
                    {
                        Console.WriteLine(c.name);
                    }
                } else
                {
                    if (c.name == s)
                    {
                        Console.WriteLine(c.name);
                    }
                }
            }
        }
    }
}
class commandFile
{
    public string[] lines;
    public List<command> commands=new List<command>();
    public commandFile(string[] file) {
        lines = file;
        findCommands();
    }
    public void findCommands() {
        bool commandOpened = false;
        string name = "";
        List<string> parameters = new List<string>();
        List<string> code = new List<string>();
        foreach (string line in lines) {
            if (commandOpened)
            {   
                if (line.StartsWith("}"))
                {
                    commandOpened = false;
                    commands.Add(new command(name, parameters, code));
                } else {
                    code.Add(line);
                }
            } else if (line.StartsWith("{")) {
                commandOpened = true;
                name = line.Substring(2, line.IndexOf(']') - 2);
                string[] s = line.Split('[');
                for (int i = 1; i < s.Length; i++) {
                    parameters.Add(s[i].Substring(0,s[i].IndexOf(']')));
                }
            }
        }
    }
}
class command
{
    public string name;
    public List<string> parameters = new List<string>();
    public List<string> code = new List<string>();
    public command(string n, List<string> p, List<string> c)
    {
        name = n;
        parameters = p;
        code = c;
    }
    public void run() {
        foreach (string line in code) {

        }
    } 
}
