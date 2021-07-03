using System;
using System.IO;

namespace GUIApp
{
    class Program
    {
        enum mode {General, Calculator, Compiler, File};
        static int currentMode = (int)mode.General;
        static void Main(string[] args)
        {
            while(true) {
                if(currentMode == (int)mode.General) {
                    Console.Write("Enter Command: ");
                    string input = Console.ReadLine();

                    string[] splitInput = input.Split(" ");

                    Console.WriteLine(readCommand(splitInput));
                }
                else if(currentMode == (int)mode.File) {
                    Console.Write("Enter File Action: ");
                    string input = Console.ReadLine();

                    string[] splitInput = input.Split(" ");

                    Console.WriteLine(readFileCommand(splitInput));
                }
            }
        }

        static string readCommand(string[] input) {
            string command = input[0].ToLower();
            string output = "";
            switch(command) {
                case "filemode":
                    currentMode = (int)mode.File;
                    break;
                case "print":
                    for(int i = 1; i <= input.Length - 1; i++) 
                        output += input[i] + " ";
                    break;
                case "exit":
                    Console.Write("Are you sure? (y/n): ");
                    if(Console.ReadLine() == "y")
                        System.Environment.Exit(1);
                    break;
                default:
                    output = "Command not found";
                    break;
            }

            return output;
        }
    
        static string readFileCommand(string[] input) {
            string command = input[0].ToLower();
            string output = "";

            switch(command) {
                case "read":
                    String line;
                    try {
                        StreamReader sr = new StreamReader("C:\\Users\\rperg\\Desktop\\C# gui thing\\GUIApp\\" + input[1]);

                        line = sr.ReadLine();

                        Console.Write("\n");

                        while(line != null) {
                            output += line + "\n";
                            line = sr.ReadLine();
                        }
                        sr.Close();
                    }
                    catch(Exception e) {
                        output = "Exception Occured: " + e.Message;
                    }
                    break;
                case "overwrite":
                    string fileWrite = "";
                    try {
                        StreamWriter sw = new StreamWriter("C:\\Users\\rperg\\Desktop\\C# gui thing\\GUIApp\\" + input[1]);
                        
                        for(int i = 2; i <= input.Length - 1; i++) {
                            if(input[i] == "\n")
                                fileWrite += "\n";
                            else
                                fileWrite += input[i] + " ";
                        }
                        sw.WriteLine(fileWrite);
                        sw.Close();
                    }
                    catch(Exception e) {
                        output = "Exception: " + e.Message;
                    }
                    output = "Wrote " + fileWrite + "to " + input[1];
                    break;
                case "append":
                    string appendText = "";
                    try {
                        StreamWriter sw = new StreamWriter("C:\\Users\\rperg\\Desktop\\C# gui thing\\GUIApp\\" + input[1], true);
                        
                        for(int i = 2; i <= input.Length - 1; i++) {
                            if(input[i] == "\n")
                                appendText += "\n";
                            else
                                appendText += input[i] + " ";
                        }
                        sw.WriteLine(appendText);
                        sw.Close();
                    }
                    catch(Exception e) {
                        output = "Exception: " + e.Message;
                    }
                    output = "Appended " + appendText + "to " + input[1];
                    break;
                case "create":
                    try {
                        File.WriteAllLines("C:\\Users\\rperg\\Desktop\\C# gui thing\\GUIApp\\" + input[1], new string[] {""});
                        output = "Created file " + input[1];
                    }
                    catch(Exception e) {
                        output = e.Message;
                    }
                    break;
                case "delete":
                    try {
                        File.Delete("C:\\Users\\rperg\\Desktop\\C# gui thing\\GUIApp\\" + input[1]);
                        output = "Deleted file " + input[1];
                    }
                    catch(Exception e) {
                        output = e.Message;
                    }
                    break;
                case "exit":
                    Console.Write("Are you sure you want to exit FILE MODE? (y/n): ");
                    if(Console.ReadLine() == "y")
                        currentMode = (int)mode.General;
                    break;
            }

            return output;
        }
    }
}
