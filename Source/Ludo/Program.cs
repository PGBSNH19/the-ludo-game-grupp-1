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
            GameEngine gameEngine = new GameEngine(new Session(), new GameBoard(), new GameLog(), new LudoContext());
            GameLoop gameLoop = new GameLoop(gameEngine, new Menu());
            gameLoop.Run();
        }
    }
}
