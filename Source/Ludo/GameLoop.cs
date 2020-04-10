using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using EngineClasses;

namespace Ludo
{
    public class GameLoop
    {
        private GameEngine gameEngine;
        private int diceResult;
        private Player currentPlayer;
        public bool IsRunning { get; private set; }
        private string[] colors = { "Red", "Blue", "Green", "Yellow" };

        public GameLoop(GameEngine gameEngine)
        {
            this.gameEngine = gameEngine;
            this.IsRunning = false;
        }

        public bool StartLoopThread()
        {
            IsRunning = true;
            Thread gameLoop = new Thread(StartLoop);
            gameLoop.Start();
            return IsRunning;
        }

        public bool StopLoopThread()
        {
            return this.IsRunning = false;
        }

        private void StartLoop()
        {
            IsRunning = true;
            while (IsRunning)
            {
                
                //Find player whos turn it is
                currentPlayer = gameEngine.CurrentPlayer();


                diceResult = gameEngine.RollDice();
                //Get moveable gamepieces and move first piece in list
                if (gameEngine.MovableGamePieces(currentPlayer, diceResult).Any())
                {
                    gameEngine.MoveGamePiece(gameEngine.MovableGamePieces(currentPlayer, diceResult)[0], diceResult);
                }

                UpdateConsole(gameEngine, diceResult);
                if(gameEngine.IsWinner(currentPlayer))
                {
                    Console.WriteLine(currentPlayer.UserName + " is winner!!");
                    IsRunning = false;
                    gameEngine.CreateGameLog(currentPlayer.UserName);
                    gameEngine.RemoveSession();
                    Environment.Exit(0);
                }
                SaveGame();
                gameEngine.Session.Turns++;
                Thread.Sleep(100);
            }
        }

        public void PlayerSelect(int players)
        {
            int i = 0;
            while(i < players)
            {
                Console.WriteLine("Add username to player" + (i + 1));
                gameEngine.CreatePlayer(Console.ReadLine(), colors[i]);
                i++;
            }
            
        }
        private static void UpdateConsole(GameEngine gameEngine, int diceResult)
        {
            Console.Clear();
            Console.WriteLine("\n\n\n");

            Console.WriteLine($"Current Player: {gameEngine.CurrentPlayer().UserName}");
            Console.WriteLine($"Dice Roll: {diceResult}");
            Console.WriteLine();

            foreach (BoardSquare square in gameEngine.GameBoard.Board)
            {
                if (square.GamePieces.Any())
                {
                    Console.Write(square.GamePieces[0].Player.UserName[0]);
                }
                else
                {
                    Console.Write(" ");
                }
            }

            Console.WriteLine();
            PrintConsoleBoard(gameEngine.GameBoard.Board);
            
        }

        private static void PrintConsoleBoard(List<BoardSquare> board)
        {
            foreach (BoardSquare square in board)
            {
                Console.ForegroundColor = ConsoleColor.Black;

                if (square.Color == "Red")
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                }
                else if (square.Color == "Yellow")
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                }
                else if (square.Color == "Green")
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                }
                else if (square.Color == "Blue")
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                }

                if (square.StartingSquare)
                {
                    Console.Write("S");
                }
                else if (square.EndSquare)
                {
                    Console.Write("E");
                }
                else if (true)
                {
                    Console.Write("-");
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        private void PrintStatistics(List<Player> players)
        {

        }
        
        public void MainMenu()
        {
            string[] menuOptions = { "new game", "load game", "exit" };
            string[] numberOfPlayers = { "2", "3", "4" };
            string menuChoice = "";
            bool menuIsRunning = true;
            while (menuIsRunning)
            {
                menuChoice = ShowMenu("LudoGame", menuOptions);

                switch (menuChoice)
                {
                    case "new game":
                        int players = int.Parse(ShowMenu("PlayerSelect", numberOfPlayers));
                        PlayerSelect(players);
                        StartLoopThread();
                        menuIsRunning = false;
                        break;
                    case "load game":
                        LoadGame();
                        StartLoopThread();
                        menuIsRunning = false;
                        break;
                    case "exit":
                        break;
                }
            }
            if(PauseGame(new ConsoleKeyInfo()))
            {
                
            }
        }

        public bool PauseGame(ConsoleKeyInfo keyPress)
        {
            while (keyPress.Key != ConsoleKey.Escape)
            {
                keyPress = Console.ReadKey();

                if (keyPress.Key == ConsoleKey.Spacebar)
                {
                    if (IsRunning)
                    {
                        return StopLoopThread();
                    }
                    else
                    {
                        return StartLoopThread();
                    }
                }
            }
            return IsRunning;
        }
    
        public void SaveGame()
        {
            gameEngine.SaveSession();
        }

        public void LoadGame()
        {
            gameEngine.PlayCurrentSession();
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

            Console.CursorVisible = true;
            return options[selected];
        }
    }
}
