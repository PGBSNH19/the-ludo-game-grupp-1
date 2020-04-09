using System;
using System.Collections.Generic;
using System.Text;
using EngineClasses;

namespace LudoGUI
{
    public static class GameLoop
    {
        public static void StartRound(GameEngine gameEngine)
        {
            Player currentPlayer = gameEngine.CurrentPlayer();



            gameEngine.Session.Turns++;
        }
    }
}
