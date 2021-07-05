using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Collections.Generic;

namespace GUIApp
{
    class Program
    {
        enum mode {General, Calculator, File, Games};
        enum gameMode {noGame, RPS, TTT};

        static int currentMode = (int)mode.General;

        static int currentGame = (int)gameMode.noGame;

        static void Main(string[] args)
        {
            Initialize();

            Login();

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
                else if(currentMode == (int)mode.Games) {
                    if(currentGame == (int)gameMode.RPS)
                        playRPS();
                    else if(currentGame == (int)gameMode.TTT)
                        playTTT();
                    else if(currentGame == (int)gameMode.noGame) {
                        Console.Write("Which game would you like to play: ");
                        string input = Console.ReadLine();

                        string[] splitInput = input.Split(" ");

                        Console.WriteLine(chooseGame(splitInput));
                    }
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
                    else if(input[1].ToLower() == "games") {
                        currentMode = (int)mode.Games;
                        output = "Sending you to games mode...";
                    }
                    else
                        output = "Mode not found";
                    break;
                case "print":
                    for(int i = 1; i <= input.Length - 1; i++) 
                        output += input[i] + " ";
                    break;
                case "clear":
                    Console.WriteLine("Clearing Console...");
                    Thread.Sleep(1000);
                    Console.Clear();
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

        static string chooseGame(string[] input) {
            string output = "";

            if(input[0] == "rps") {
                currentGame = (int)gameMode.RPS;
                output = "Starting \"Rock, Paper, Scissors\"";
            }
            else if(input[0] == "ttt") {
                currentGame = (int)gameMode.TTT;
                output = "Starting \"Tic, Tac, Toe\"";
            }
            else if(input[0] == "exit") {
                Console.Write("Are you sure you want to exit GAME MODE? (y/n): ");
                if(Console.ReadLine() == "y") {
                    Console.Clear();
                    currentGame = (int)gameMode.noGame;
                    currentMode = (int)mode.General;
                }
            }

            return output;
        }

        static void playRPS() {
            Console.Clear();
            Console.WriteLine("Welcome to Rock, Paper, Scissors! Here are the rules:\nYou and an AI will both choose either Rock, Paper, or Scissors.\nScissors beats Paper, Paper beats Rock, and Rock beats Scissors.\n Whoever beats the other person wins!");
            Console.Write("Type \"r\" for Rock, \"p\" for Paper, and \"s\" for Scissors:");
            string playerChoice = Console.ReadLine();

            Random rand = new Random();
            int randNum = rand.Next(3);
            string aiChoice = "";

            if(randNum == 0)
                aiChoice = "r";
            else if(randNum == 1)
                aiChoice = "p";
            else if(randNum == 2)
                aiChoice = "s";
            
            if((aiChoice == "p" && playerChoice == "r") || (aiChoice == "s" && playerChoice == "p") || (aiChoice == "r" && playerChoice == "s"))
                Console.WriteLine("You Lose! The AI chose " + aiChoice);
            else if(aiChoice == playerChoice)
                Console.WriteLine("You Tie! The AI also chose " + aiChoice);
            else
                Console.WriteLine("You Win! The AI chose " + aiChoice);

            Console.Write("That was a good game. Would you like to play again? (y/n): ");
            if(Console.ReadLine() == "n")
                currentGame = (int)gameMode.noGame;
            Console.Clear();
        }

        static void playTTT() {
            int player = 1;
            string[,] grid = new string[3, 3];
            for(int x = 0; x < grid.GetLength(0); x++) {
                for(int y = 0; y < grid.GetLength(1); y++) {
                    grid[x, y] = "   ";
                }
            }
        
            Console.WriteLine("Welcome to Tic-Tac-Toe! To play this game, you have to choose a spot to put your X or O. You need to get 3 of your symbol in a row to win! (This is a two player game)");
            Console.WriteLine(grid[0, 0] + "\u2502" + grid[1, 0] + "\u2502" + grid[2, 0] + "\n\u2500\u2500\u2500\u253C\u2500\u2500\u2500\u253C\u2500\u2500\n" + grid[0, 1] + "\u2502"  + grid[1, 1] + "\u2502"  + grid[2, 1] + "\n\u2500\u2500\u2500\u253C\u2500\u2500\u2500\u253C\u2500\u2500\n"  + grid[0, 2] + "\u2502"  + grid[1, 2] + "\u2502"  + grid[2, 2] + "\n");
            Console.Write("Type \"Start\" to start the game: ");
            Console.ReadLine();
            while(true) {
                Console.Clear();
                Console.WriteLine(grid[0, 0] + "\u2502" + grid[1, 0] + "\u2502" + grid[2, 0] + "\n\u2500\u2500\u2500\u253C\u2500\u2500\u2500\u253C\u2500\u2500\n" + grid[0, 1] + "\u2502"  + grid[1, 1] + "\u2502"  + grid[2, 1] + "\n\u2500\u2500\u2500\u253C\u2500\u2500\u2500\u253C\u2500\u2500\n"  + grid[0, 2] + "\u2502"  + grid[1, 2] + "\u2502"  + grid[2, 2] + "\n");
                Console.Write("Player " + player + " to chose where you want to place your symbol, type \"row,column\": ");

                string[] input = Console.ReadLine().Split(",");
                if(grid[Int32.Parse(input[1]) - 1, Int32.Parse(input[0]) - 1] == "   ") {
                    if(player == 1) {
                        grid[Int32.Parse(input[1]) - 1, Int32.Parse(input[0]) - 1] = " X ";
                        player = 2;
                    }
                    else if(player == 2) {
                        grid[Int32.Parse(input[1]) - 1, Int32.Parse(input[0]) - 1] = " O ";
                        player = 1;
                    }
                    else 
                        Console.WriteLine("Something went wrong. Please try again.");
                }
                else
                    Console.WriteLine("You can't place a piece there");
                
                
            }
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
        
        static void Login() {
            Console.Write("Enter Password: ");

            string pass = null;

            while(true) {
                var key = Console.ReadKey(true);
                if(key.Key == ConsoleKey.Enter)
                    break;
                Console.Write("*");
                pass += key.KeyChar;
            }


            if(System.Convert.ToBase64String(Encoding.UTF8.GetBytes(pass)) == "cG9vcG9vNjlsb2w=") {
                Thread.Sleep(750);
                Console.Clear();
                Console.WriteLine("Welcome, Rpergy");
            }
            else {
                Thread.Sleep(750);
                Console.WriteLine("\nWrong Password...\nTerminating program");
                Thread.Sleep(1500);
                System.Environment.Exit(69);
            }
        }
    }
}
