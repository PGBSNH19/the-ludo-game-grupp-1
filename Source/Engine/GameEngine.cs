using Microsoft.EntityFrameworkCore;
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
            return Session.Player[index];
        }

        public Player CurrentPlayerTurn()
        {
            return Session.CurrentPlayerTurn();
        }

        public void MoveGamePiece(GamePiece gamePiece, int steps)
        {
            if (!gamePiece.IsAtGoal)
            {
                if (gamePiece.IsAtBase && steps >= 6)
                {
                    gamePiece.Position = GameBoard.ValidateStartingSquare(gamePiece.Player).GameSquareNumber;
                    gamePiece.IsAtBase = false;
                }
                else
                {
                    for (int i = 0; i < steps; i++)
                    {
                        gamePiece.Position = GameBoard.FindNextValidSquare(gamePiece).GameSquareNumber;
                        if (GameBoard.ValidateNextSquare(gamePiece).EndSquare ||
                            i == steps - 1)
                        {
                            gamePiece.IsAtGoal = true;
                        }
                    }
                }
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
