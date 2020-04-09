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
            int diceResult;
            
            // Skapar ett nytt spel
            GameEngine gameEngine = new GameEngine(new Session(), new GameBoard(), new GameLog());

            gameEngine.Session.CreatePlayer("Mirko", "Red");
            gameEngine.Session.CreatePlayer("Hans", "Blue");
            gameEngine.Session.CreatePlayer("Ture", "Green");
            gameEngine.Session.CreatePlayer("Berit", "Yellow");

            Player currentPlayer;
            while (true)
            {
                //Find player whos turn it is
                currentPlayer = gameEngine.CurrentPlayerTurn();

                //Roll dice
                diceResult = gameEngine.RollDice();

                //Get moveable gamepieces and move first piece in list
                if (gameEngine.MovableGamePieces(currentPlayer, diceResult).Any())
                {
                    gameEngine.MoveGamePiece(gameEngine.MovableGamePieces(currentPlayer, diceResult)[0], diceResult);
                }

                UpdateConsole(gameEngine, diceResult);

                gameEngine.Session.Turns++;
                Thread.Sleep(500);
                Console.ReadKey();
            }
        }
        
        public static void UpdateConsole(GameEngine gameEngine, int diceResult)
        {
            Console.Clear();
            Console.WriteLine("\n\n\n ");

            Console.WriteLine($"Current Player: {gameEngine.CurrentPlayerTurn().UserName}");
            Console.WriteLine($"Dice roll: {diceResult}" );
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
                else if (square.StartingSquare)
                {
                    Console.Write("E");
                }
                else if (true)
                {
                    Console.Write("-");
                }
            }
        }
    }
}
