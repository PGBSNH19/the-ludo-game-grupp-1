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
        private Menu menu;

        public bool IsRunning { get; private set; }
        private string[] playerColors = { "Red", "Blue", "Green", "Yellow" };

        public GameLoop(GameEngine gameEngine, Menu menu)
        {
            this.gameEngine = gameEngine;
            this.menu = menu;
            this.IsRunning = false;
        }

        public void MainMenu()
        {
            string menuChoice = "";
            bool menuIsRunning = true;
            while (menuIsRunning)
            {
                menuChoice = menu.ShowMainMenu();

                switch (menuChoice)
                {
                    case "new game":
                        int players = int.Parse(menu.ShowNewGameMenu());
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
            if (PauseGame(new ConsoleKeyInfo()))
            {

            }
        }

        private bool StartLoopThread()
        {
            this.IsRunning = true;
            Thread gameLoop = new Thread(StartLoop);
            gameLoop.Start();
            return this.IsRunning;
        }

        private bool PauseGame(ConsoleKeyInfo keyPress)
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

        private bool StopLoopThread()
        {
            this.IsRunning = false;
            return this.IsRunning;
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
                if (gameEngine.IsWinner(currentPlayer))
                {
                    Console.WriteLine(currentPlayer.UserName + " is winner!!");
                    IsRunning = false;
                    gameEngine.CreateGameLog(currentPlayer.UserName);
                    gameEngine.RemoveSession();
                }
                else
                {
                    SaveGame();
                }
                gameEngine.Session.Turns++;
                Thread.Sleep(100);
            }
        }

        private void PlayerSelect(int players)
        {
            int i = 0;
            while (i < players)
            {
                Console.WriteLine("Add username to player" + (i + 1));
                gameEngine.CreatePlayer(Console.ReadLine(), playerColors[i]);
                i++;
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
            Console.Clear();
            foreach (var p in players)
            {
                Console.WriteLine($"{p.UserName} has {p.GamePiece.Where(gp => gp.IsAtBase).Count()} gamepieces who is at base!");
                Console.WriteLine($"{p.UserName} has {p.GamePiece.Where(gp => gp.IsAtGoal).Count()} gamepieces who is at GOAL!");
            }
        }

        private void SaveGame()
        {
            gameEngine.SaveSession();
        }

        private void LoadGame()
        {
            gameEngine.PlayCurrentSession();
        }
    }
}
