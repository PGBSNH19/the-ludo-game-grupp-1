using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EngineClasses;
using Microsoft.Extensions.Configuration;

namespace Ludo
{
    class Program
    {
        static void Main(string[] args)
        {
            Session session = new Session();
            session.CreatePlayer("Mirko", "Red");
            session.CreatePlayer("Aron", "Yellow");
            session.CreatePlayer("Hampus", "Green");
            session.CreatePlayer("Anas", "Blue");

            GameEngine gameEngine = new GameEngine(session, new GameBoard(), new GameLog());
            foreach (var item in gameEngine.GameBoard.BoardRoute)
            {
                Console.WriteLine(item.GameSquareId);
            }

            var currentPlayer = gameEngine.CurrentPlayerTurn();
            Console.WriteLine(currentPlayer); 
        }

        public static void Test1DGameBoard()
        {
            //All gamepieces can only move on indexes with an a, or with there own color letter
            //If they land on an index with the letter "f" they have gone to their finish-line
            //r = red, y = yellow, g = green, b = blue
            string[] board = {"ar", "a", "a", "a", "a", "a", "a", "a", "a", "a",
                              "y", "y", "y", "y", "yf",
                              "ay", "a", "a", "a", "a", "a", "a", "a", "a", "a",
                              "g", "g", "g", "g", "gf",
                              "ag", "a", "a", "a", "a", "a", "a", "a", "a", "a",
                              "b", "b", "b", "b", "bf",
                              "ab", "a", "a", "a", "a", "a", "a", "a", "a", "a",
                              "r", "r", "r", "r", "rf",};

            Dictionary<string, int> players = new Dictionary<string, int>();
            players.Add("r", board.ToList().IndexOf("ar"));
            players.Add("y", board.ToList().IndexOf("ay"));
            players.Add("g", board.ToList().IndexOf("ag"));
            players.Add("b", board.ToList().IndexOf("ab"));


            for (int i = 0; i < 6; i++)
            { 
                if (board[players["r"] + 1].Contains('a') ||
                    board[players["r"] + 1].Contains('r'))
                {
                    Console.WriteLine($"Red player is at: {players["r"]}");
                    players["r"]++;
                }
                else
                {
                    players["r"]++;
                }
            }

            if (board[players["r"]].Contains('f'))
            {
                Console.WriteLine($"Red player reached goal at: {players["r"]}");
            }
        }
    }
}
