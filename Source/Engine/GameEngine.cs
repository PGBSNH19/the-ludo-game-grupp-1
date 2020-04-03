﻿using System;
using System.Collections.Generic;
using System.Linq;
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

        public void CreatePlayer(string userName, string color)
        {
            Session.CreatePlayer(userName, color);
        }

        public Player PlayerSelect(int index)
        {
            return Session.Player.ToList()[index];
        }

        public Player CurrentPlayerTurn()
        {
            return Session.CurrentPlayerTurn();
        }

        
        public void MoveGamePiece(GamePiece gamePiece, int dice)
        {
            if (gamePiece.IsAtBase)
            {
                GameBoard.ValidateStartingSquare(gamePiece);
            }
            else
            {
<<<<<<< Updated upstream
                NextTurn(1);
=======
                
>>>>>>> Stashed changes
            }
        }
        
        public void NextTurn(int index)
        {
            Session.Turns++;
            //Session.AddToDb();
            Player player = CurrentPlayerTurn();
            int dice = player.RollDice();
            GamePiece piece = player.SelectGamePiece(index);
            MoveGamePiece(piece, dice);
            
            if (player.GamePiece.Where(gp => gp.IsAtGoal == true).Count() == 4)
            {
                GameLog = new GameLog(player.UserName);
                GameLog.AddToDb();
                Environment.Exit(0);
            }
        }
    }
}
