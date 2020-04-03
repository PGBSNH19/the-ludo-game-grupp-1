﻿using System;
using System.IO;
using EngineClasses;
using Microsoft.Extensions.Configuration;

namespace Ludo
{
    class Program
    {
        static void Main(string[] args)
        {

            var myString = ConnectionSetup.GetConnectionString();
            Console.WriteLine(myString);

            
            GameBoard gameBoard = new GameBoard();
            int horizontal = gameBoard.Placements.GetUpperBound(0);
            int vertical = gameBoard.Placements.GetUpperBound(0);

            for (int i = 0; i <= horizontal; i++)
            {
                for (int j = 0; j <= vertical; j++)
                {
                    string res = gameBoard.Placements[i, j];
                    Console.Write(res + " ");
                }
                Console.WriteLine();
            }

            Session session = new Session();
            session.CreatePlayer("Mirko", "Red");
            session.CreatePlayer("Aron", "Yellow");
            session.CreatePlayer("Hampus", "Green");
            session.CreatePlayer("Anas", "Blue");

            GameEngine gEngine = new GameEngine(session, gameBoard, new GameLog());

            gEngine.NextTurn(1);

        }
    }
}
