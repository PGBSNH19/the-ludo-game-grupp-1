﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                NextTurn(1);
            }
        }
        
        
        public void NextTurn(int index)
        {
            Session.Turns++;
            
            Player player = CurrentPlayerTurn();
            int dice = player.RollDice();
            GamePiece piece = player.SelectGamePiece(index);
            GameBoard.ContinueRoute(piece,dice);
            
            if (player.GamePiece.Where(gp => gp.IsAtGoal == true).Count() == 4)
            {
                GameLog = new GameLog(player.UserName);
                GameLog.AddToDb();
                Environment.Exit(0);
            }
        }
        public Session LoadSessionFromDb()
        {
            Session session = null;
            using (var context = new LudoContext())
            {

                session = context.Session
                        .Include(s => s.Player)
                        .ThenInclude(p => p.GamePiece)
                        .FirstOrDefault();


                context.SaveChanges();
            }

            return session;
        }
    }
}
