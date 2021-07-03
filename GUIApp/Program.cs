using System;
using System.IO;

namespace GUIApp
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true) {
                Console.Write("Enter Command: ");
                string input = Console.ReadLine();

                string[] splitInput = input.Split(" ");

                Console.WriteLine(readCommand(splitInput));
            }
        }

        static string readCommand(string[] input) {
            string command = input[0].ToLower();
            string output = "";

            switch(command) {
                case "print":
                    for(int i = 1; i <= input.Length - 1; i++) 
                        output += input[i] + " ";
                    break;
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
                        Console.WriteLine("Exception Occured: " + e.Message);
                    }
                    break;
                case "settext":
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
                        Console.WriteLine("Exception: " + e.Message);
                    }
                    Console.WriteLine("Wrote " + fileWrite + "to " + input[1]);
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
                        Console.WriteLine("Exception: " + e.Message);
                    }
                    Console.WriteLine("Appended " + appendText + "to " + input[1]);
                    break;
                case "create":
                    try {
                        File.WriteAllLines("C:\\Users\\rperg\\Desktop\\C# gui thing\\GUIApp\\" + input[1], new string[] {""});
                        Console.WriteLine("Created file " + input[1]);
                    }
                    catch(Exception e) {
                        Console.WriteLine(e.Message);
                    }
                    break;
                case "delete":
                    try {
                        File.Delete("C:\\Users\\rperg\\Desktop\\C# gui thing\\GUIApp\\" + input[1]);
                        Console.WriteLine("Deleted file " + input[1]);
                    }
                    catch(Exception e) {
                        Console.WriteLine(e.Message);
                    }
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
    }
}
