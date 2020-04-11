using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EngineClasses;

namespace Ludo
{
    public class GameLoop
    {
        private GameEngine gameEngine;
        private int diceResult;
        private Player currentPlayer;
        private Menu menu;
        private bool isRunning;
        private bool gameOver;
        private string[] playerColors = { "Red", "Blue", "Green", "Yellow" };

        public GameLoop(GameEngine gameEngine, Menu menu)
        {
            this.gameEngine = gameEngine;
            this.menu = menu;
            this.isRunning = false;
            this.gameOver = false;
        }

        private void ConstructNewEmptySession()
        {
            gameEngine = new GameEngine(new Session(), new GameBoard(), new GameLog(), new LudoContext());
        }

        public void Run()
        {
            bool exitGame = false;
            Console.CursorVisible = false;
            while (!exitGame)
            {
                exitGame = MainMenu();
            }           
        }

        private bool MainMenu()
        {
            bool exit = false;
            string menuChoice = "";
            menuChoice = menu.ShowMainMenu();
            switch (menuChoice)
            {
                case "new game":
                    RunNewGame();
                    exit = false;
                    break;
                case "load game":
                    LoadGame();
                    exit = false;
                    break;
                case "exit":
                    exit = true;
                    break;
            }
            return exit;
        }

        private void RunNewGame()
        {
            ConstructNewEmptySession();
            int players = int.Parse(menu.ShowNewGameMenu());
            AddPlayers(players);
            StartLoopThread();
        }

        private void AddPlayers(int players)
        {
            Console.CursorVisible = true;

            int i = 0;
            while (i < players)
            {
                Console.WriteLine("Add username to Player: " + (i + 1));
                gameEngine.CreatePlayer(Console.ReadLine(), playerColors[i]);
                i++;
            }

            Console.CursorVisible = false;
        }

        private void StartLoopThread()
        {
            Task gameLoop = Task.Run(() => StartLoop());
            ToggleGameLoopRunning();
            gameLoop.Wait();
        }

        private void StopLoopThread() => this.isRunning = false;

        private void ToggleGameLoopRunning()
        {
            ConsoleKeyInfo keyPress = new ConsoleKeyInfo();

            while (keyPress.Key != ConsoleKey.Escape && !gameOver)
            {
                    keyPress = Console.ReadKey();             

                if (keyPress.Key == ConsoleKey.Spacebar)
                {
                    if (isRunning)
                    {
                        StopLoopThread();
                    }
                    else
                    {
                        StartLoopThread();
                    }
                }
                if (keyPress.Key == ConsoleKey.Escape)
                {
                    StopLoopThread();
                }
            }
        }

        private void StartLoop()
        {
            isRunning = true;
            Task DBTasks;
            while (isRunning)
            {
                gameEngine.Session.Turns++;
                currentPlayer = gameEngine.CurrentPlayer();
                diceResult = gameEngine.RollDice();

                if (gameEngine.MovableGamePieces(currentPlayer, diceResult).Any())
                {
                    gameEngine.MoveGamePiece(gameEngine.MovableGamePieces(currentPlayer, diceResult)[0], diceResult);
                }

                UpdateConsole(gameEngine, diceResult);
                if (gameEngine.IsWinner(currentPlayer))
                {
                    isRunning = false;
                    gameOver = true;

                    PrintWinner(currentPlayer);
                    PrintStatistics(gameEngine.Session.Player);

                    DBTasks = Task.Run(() => gameEngine.CreateGameLog(currentPlayer.UserName));
                    Console.WriteLine("Logging game...");
                    DBTasks.Wait();

                    DBTasks = Task.Run(() => gameEngine.RemoveSession());
                    Console.WriteLine("Removing session from database...");
                    DBTasks.Wait();

                    Console.WriteLine("All done! Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    DBTasks = Task.Run(() => SaveGame());
                    Thread.Sleep(100);
                    DBTasks.Wait();                    
                }                
            }
        }

        private static void UpdateConsole(GameEngine gameEngine, int diceResult)
        {
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Clear();
            Console.WriteLine("\n\n\n");

            Console.WriteLine($"Current Player: {gameEngine.CurrentPlayer().UserName}");
            Console.WriteLine($"Dice Roll: {diceResult}");
            Console.WriteLine();
            PrintStatistics(gameEngine.Session.Player);

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
            Console.WriteLine("\n\n\n");
            Console.WriteLine("Press [Spacebar] to toggle game running. \n" +
                "Press [ESC] to return to main menu.");
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

        private static void PrintStatistics(List<Player> players)
        {
            foreach (var p in players.OrderByDescending(p => p.GamePieces.Where(gp => gp.IsAtGoal).Count()))
            {
                Console.WriteLine($"{p.UserName} has {p.GamePieces.Where(gp => gp.IsAtGoal).Count()} gamepieces at GOAL!");
                Console.WriteLine($"{p.UserName} has {p.GamePieces.Where(gp => gp.IsAtBase).Count()} gamepieces at BASE!");
            }
            Console.WriteLine("");
        }

        private void PrintWinner(Player player)
        {
            Console.Clear();
            Console.WriteLine("\nTHE WINNER IS " + player.UserName + "!!! =D\n");
        }

        private void SaveGame() => gameEngine.SaveSession();

        private void LoadGame()
        {
            if (gameEngine.LoadSession() != null)
            {
                ConstructNewEmptySession();
                gameEngine.PlayCurrentSession();
                StartLoopThread();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Cannot find any saved sessions. \n" +
                                  "Press any key to return to main menu.");
                Console.ReadKey();
            }
        }
    }
}
