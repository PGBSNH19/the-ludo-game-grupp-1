﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EngineClasses
{
    public class GameEngine
    {
        public Session Session { get; private set; }
        public GameBoard GameBoard { get; private set; }
        public GameLog GameLog { get; private set; }

        public GameEngine(Session session, GameBoard gameBoard, GameLog gameLog)
        {
            this.Session = session;
            this.GameBoard = gameBoard;
            this.GameLog = gameLog;
        }

        public void PlayerSelect(string userName)
        {
            Session.CreatePlayer(userName);
        }

        public Player CurrentPlayerTurn()
        {
            return Session.CurrentPlayerTurn();
        }

        public int RollDice()
        {
            int result;
            Random rnd = new Random();

            result = rnd.Next(1, 6 + 1);
            return result;
        }

        public void Move(int xCoord, int yCoord)
        {

        }

    }
}
