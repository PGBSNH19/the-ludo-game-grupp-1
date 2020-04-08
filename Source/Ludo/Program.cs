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
            // Skapar ett nytt spel
            GameEngine gameEngine = new GameEngine(new Session(), new GameBoard(), new GameLog());
            if (gameEngine.LoadSession() != null)
            {
                Console.WriteLine("Their is a session availible to play, do you wanna play?");
                gameEngine.PlayCurrentSession();
                gameEngine.CurrentPlayerTurn();

            }

        }
    }
}
