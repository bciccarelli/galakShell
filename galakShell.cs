using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
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
        while (true)
        {
            Console.Write(">");
            interpret(Console.ReadLine());
        }
    }
    public static string[] readFile(string fileName) {
        return File.ReadAllLines(fileName);
    }
    public static void writeFile(string fileName, string line)
    {
        using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(fileName, true))
        {
            file.WriteLine(line);
        }
    }
    public static void setup()
    {
        paths = new List<string>();
        commandFiles = new List<commandFile>();
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
        if (line.StartsWith("createCommand"))
        {
            line = line.Substring(14);
            writeFile(dataFileName,"\n"+line);
            setup();
            Console.WriteLine("done");
        }
        else
        {
            identifyCommand(line);
        }
    }
    public static void identifyCommand(string s) {
        foreach (commandFile cf in commandFiles) {
            foreach (command c in cf.commands)
            {
                if (s.IndexOf(' ') > 0)
                {
                    if (c.name == s.Substring(0, s.IndexOf(' ')))
                    {
                        List<string> param = new List<string>();
                        for (int i = 1; i < s.Split(' ').Length; i++) {
                            param.Add(s.Split(' ')[i]);
                        }
                        c.run(param);
                    }
                } else
                {
                    if (c.name == s)
                    {
                        c.run(new List<string>() { "" });
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
    public void run(List<string> param) {
        foreach (string line in code) {
            recognize(line, param);
        }
    }
    public void recognize(string line, List<string> param) {
        switch (line.Substring(0, line.IndexOf(' ')).ToLower()) {
            case "write":
                Console.WriteLine( identify(line.Substring(line.IndexOf(' ')+1), param) );
                break;
            case "run":
                Process.Start("CMD.exe", identify(line.Substring(line.IndexOf(' ')+1), param) );
                break;
        }
    }
    public string identify(string piece, List<string> param)
    {
        if (piece.Substring(0, 2) == "[p")
        {
            return param[toInt(piece.Substring(2, 1))];
        }
        else {
            while (piece.IndexOf("[")>0)
            {
                piece = piece.Replace(piece.Substring(piece.IndexOf("["),4), param[toInt(piece.Substring(piece.IndexOf("[")+2, 1))]);
            }
            return piece;
        }
    }
    public int toInt(string s) {
        int number;
        if (!Int32.TryParse(s, out number))
        {
            Console.WriteLine(s);
            Console.WriteLine("String could not be parsed.");
        }
        return number;
    }
}
