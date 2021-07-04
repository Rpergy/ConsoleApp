using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;

namespace GUIApp
{
    class Program
    {
        enum mode {General, Calculator, File};
        static int currentMode = (int)mode.General;
        static void Main(string[] args)
        {
            Initialize();
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
                else if(currentMode == (int)mode.Calculator) {
                    Console.Write("Enter Equation: ");
                    string input = Console.ReadLine();

                    char[] splitInput = input.ToCharArray();

                    Console.WriteLine(readEquation(splitInput));
                }
            }
        }

        static string readCommand(string[] input) {
            string command = input[0].ToLower();
            string output = "";
            switch(command) {
                case "mode":
                    if(input[1].ToLower() == "file") {
                        currentMode = (int)mode.File;
                        output = "Sending you to file mode...";
                    }
                    else if(input[1].ToLower() == "calculator") {
                        currentMode = (int)mode.Calculator;
                        output = "Sending you to calculator mode...";
                    }
                    else
                        output = "Mode not found";
                    break;
                case "print":
                    for(int i = 1; i <= input.Length - 1; i++) 
                        output += input[i] + " ";
                    break;
                case "exit":
                    Console.Write("Are you sure you want to exit? (y/n): ");
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
    
        static string readEquation(char[] input) {
            string output = "";

            if(string.Join("", input) == "exit") {
                Console.Write("Are you sure you want to exit CALCULATOR MODE? (y/n): ");
                if(Console.ReadLine() == "y") {
                    currentMode = (int)mode.General;
                    return "Exiting CALCULATOR MODE...";
                }
            }

            input = removeSpaces(input);

            List<string> equation = new List<string>();
            string currentTerm = "";
            for(int i = 0; i < input.Length; i++) {
                if(input[i] == '+' || input[i] == '-' || input[i] == '*' || input[i] == '/') {
                    equation.Add(currentTerm);
                    equation.Add(input[i].ToString());
                    currentTerm = "";
                }
                else
                    currentTerm += input[i];
            }
            equation.Add(currentTerm);

            try{
                if(equation[1] == "+") 
                    output = (Int32.Parse(equation[0]) + Int32.Parse(equation[2])).ToString();
                else if(equation[1] == "-") 
                    output = (Int32.Parse(equation[0]) - Int32.Parse(equation[2])).ToString(); 
                else if(equation[1] == "*") 
                    output = (Int32.Parse(equation[0]) * Int32.Parse(equation[2])).ToString(); 
                else if(equation[1] == "/") 
                    output = (Int32.Parse(equation[0]) / Int32.Parse(equation[2])).ToString();
                else
                    output = "Operation not recognized";
            }
            catch {
                output = "Equation failed. Please try again";
            }

            return output;
        }

        static char[] removeSpaces(char[] input) {
            char[] output;
            int spaceCount = 0;

            for(int i = 0; i < input.Length; i++) {
                if(input[i] == ' ')
                    spaceCount++;
            }
            output = new char[input.Length - spaceCount];

            int ii = 0;

            for(int i = 0; i < input.Length; i++) {
                if(input[i] != ' ') {
                    output[ii] = input[i];
                    ii++;
                }
            }

            return output;
        }

        static void Initialize() {
            Console.Clear();
            Thread.Sleep(1000);
            Console.WriteLine("Please wait");
            Thread.Sleep(250);
            Console.Clear();
            Console.WriteLine("Please wait.");
            Thread.Sleep(250);
            Console.Clear();
            Console.WriteLine("Please wait..");
            Thread.Sleep(250);
            Console.Clear();
            Console.WriteLine("Please wait...");
            Thread.Sleep(250);
            Console.Clear();
        }
    }
}
