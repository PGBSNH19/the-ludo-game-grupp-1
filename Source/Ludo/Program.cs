using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using EngineClasses;
using Microsoft.Extensions.Configuration;

namespace Ludo
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] menuOptions = { "new game","load game","save game", "exit" };
            string[] numberOfPlayers = { "2", "3", "4" };
            // Skapar ett nytt spel
            GameEngine gameEngine = new GameEngine(new Session(), new GameBoard(), new GameLog());
            //gameEngine.Session.CreatePlayer("Mirko", "Red");
            //gameEngine.Session.CreatePlayer("Hans", "Blue");
            //gameEngine.Session.CreatePlayer("Ture", "Green");
            //gameEngine.Session.CreatePlayer("Berit", "Yellow");

            foreach (Player player in gameEngine.Session.Player)
            {
                gameEngine.MoveGamePiece(gameEngine.MovableGamePieces(gameEngine.CurrentPlayer(), 6)[0], 6);
                gameEngine.MoveGamePiece(gameEngine.MovableGamePieces(gameEngine.CurrentPlayer(), 2)[0], 2);

            //    gameEngine.Session.Turns++;
            }

            GameLoop gameLoop = new GameLoop(gameEngine);

            string menuChoice = "";
            bool menuIsRunning = true;
            while (menuIsRunning)
            {
                
                menuChoice = ShowMenu("LudoGame", menuOptions);

                switch (menuChoice)
                {
                    case "new game":
                        int players = int.Parse(ShowMenu("PlayerSelect", numberOfPlayers));  
                        gameLoop.PlayerSelect(players);
                        gameLoop.StartLoopThread();
                        menuIsRunning = false;
                        break;
                    case "load game":
                        gameLoop.LoadGame();
                        gameLoop.StartLoopThread();
                        menuIsRunning = false;
                        break;
                    case "save game":
                        break;
                    case "exit":
                        break; 
                }
            }
            
            ConsoleKeyInfo keyPress = new ConsoleKeyInfo();
            
            while (keyPress.Key != ConsoleKey.Escape)
            {
                keyPress = Console.ReadKey();

                if (keyPress.Key == ConsoleKey.Spacebar)
                {
                    if (gameLoop.IsRunning)
                    {
                        gameLoop.StopLoopThread();
                    }
                    else
                    {
                        gameLoop.StartLoopThread();
                    }
                }
            }
        }
        

        private static string ShowMenu(string prompt, string[] options)
        {
            Console.Clear();
            Console.WriteLine(prompt);

            int selected = 0;

            // Hide the cursor that will blink after calling ReadKey.
            Console.CursorVisible = false;

            ConsoleKey? key = null;
            while (key != ConsoleKey.Enter)
            {
                // If this is not the first iteration, move the cursor to the first line of the menu.
                if (key != null)
                {
                    Console.CursorLeft = 0;
                    Console.CursorTop = Console.CursorTop - options.Length;
                }

                // Print all the options, highlighting the selected one.
                for (int i = 0; i < options.Length; i++)
                {
                    var option = options[i];
                    if (i == selected)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine("- " + option);
                    Console.ResetColor();
                }

                // Read another key and adjust the selected value before looping to repeat all of this.
                key = Console.ReadKey().Key;
                if (key == ConsoleKey.DownArrow)
                {
                    selected = Math.Min(selected + 1, options.Length - 1);
                }
                else if (key == ConsoleKey.UpArrow)
                {
                    selected = Math.Max(selected - 1, 0);
                }
            }

            // Reset the cursor and return the selected option.
            Console.CursorVisible = true;
            return options[selected];
        }
    }
}
