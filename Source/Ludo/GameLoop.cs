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

        public void StartLoopThread()
        {
            IsRunning = true;
            Thread gameLoop = new Thread(StartLoop);
            gameLoop.Start();
        }

        public void StopLoopThread()
        {
            this.IsRunning = false;
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
                }
                gameEngine.Session.Turns++;
                Thread.Sleep(100);
                SaveGame();
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

            foreach (BoardSquare square in gameEngine.GameBoard.Board)
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

        public void SaveGame()
        {
            gameEngine.Session.AddToDb();
        }

        public void LoadGame()
        {
            gameEngine.PlayCurrentSession();
        }
    }
}
