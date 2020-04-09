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
            
            
            // Skapar ett nytt spel
            GameEngine gameEngine = new GameEngine(new Session(), new GameBoard(), new GameLog());
            gameEngine.Session.CreatePlayer("Mirko", "Red");
            //gameEngine.Session.CreatePlayer("Hans", "Blue");
            gameEngine.Session.CreatePlayer("Ture", "Green");
            //gameEngine.Session.CreatePlayer("Berit", "Yellow");

            foreach (Player player in gameEngine.Session.Player)
            {
                gameEngine.MoveGamePiece(gameEngine.MovableGamePieces(gameEngine.CurrentPlayerTurn(), 6)[0], 6);
                gameEngine.MoveGamePiece(gameEngine.MovableGamePieces(gameEngine.CurrentPlayerTurn(), 2)[0], 2);

                gameEngine.Session.Turns++;
            }

            GameLoop gameLoop = new GameLoop(gameEngine);
            gameLoop.StartLoop();
            
            
        }
        
        
    }
}
