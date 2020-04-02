using System;
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

        public void CreatePlayer(string userName)
        {
            Session.CreatePlayer(userName);
        }

        public void PlayerSelect()
        {

        }

        public Player CurrentPlayerTurn()
        {
            return Session.CurrentPlayerTurn();
        }

        

        public void MoveGamePiece(GamePiece gamePiece, int xCoord, int yCoord)
        {
            gamePiece.XCoord = xCoord;
            gamePiece.YCoord = yCoord;
        }
        
        public void NextTurn(int index)
        {
            Session.Turns++;
            //Session.AddToDb();
            Player player = CurrentPlayerTurn();
            int dice = player.RollDice();
            GamePiece piece = player.SelectGamePiece(index);
            MoveGamePiece(piece, piece.XCoord, dice);
            
            if (player.GamePiece.Where(gp => gp.IsAtGoal == true).Count() == 4)
            {
                GameLog = new GameLog(player.UserName);
                GameLog.AddToDb();
                Environment.Exit(0);
            }
        }
    }
}
