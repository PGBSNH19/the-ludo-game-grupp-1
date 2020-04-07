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

        /// <summary>
        /// Create a new player and add it to session.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="color"></param>
        public void CreatePlayer(string userName, string color)
        {
            Session.CreatePlayer(userName, color);
        }

        /// <summary>
        /// Select player based on index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Player PlayerSelect(int index)
        {
            return Session.Player[index];
        }

        /// <summary>
        /// Returns current player whos turn it is.
        /// </summary>
        /// <returns></returns>
        public Player CurrentPlayerTurn()
        {
            return Session.GetCurrentPlayer();
        }

        /// <summary>
        /// Moves a gama piece n number of steps taking into account game rules of legal movement.
        /// </summary>
        /// <param name="gamePiece"></param>
        /// <param name="steps"></param>
        public void MoveGamePiece(GamePiece gamePiece, int steps)
        {
            if (!gamePiece.IsAtGoal)
            {
                if (gamePiece.IsAtBase && steps >= 6)
                {
                    GameSquare startSquare = GameBoard.GetStartingSquare(gamePiece.Player);
                    gamePiece.Position = startSquare.GameSquareNumber;
                    startSquare.PlaceGamePiece(gamePiece);
                    gamePiece.IsAtBase = false;
                }
                else
                {
                    for (int i = 0; i < steps; i++)
                    {
                        gamePiece.Position = GameBoard.FindNextValidSquare(gamePiece).GameSquareNumber;
                        if (GameBoard.GetNextSquare(gamePiece).EndSquare ||
                            i == steps - 1)
                        {
                            gamePiece.IsAtGoal = true;
                        }
                    }

                    GameSquare currentSquare = GameBoard.GetCurrentSquare(gamePiece);              
                    currentSquare.PlaceGamePiece(gamePiece);
                }
            }
        }

        /// <summary>
        /// Returns bool based on if all players game pieces have entered their goal.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public bool CheckIfWon(Player player)
        {
            return player.GamePiece.All(gp => gp.IsAtGoal == true);
        } 
       

        /// <summary>
        /// Randomize a number between 1 - 6.
        /// </summary>
        /// <returns></returns>
        public int RollDice()
        {
            return Session.RollDice();
        }

        /// <summary>
        /// Return a list specifying wich game pieces a player have that can legally move.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="dice"></param>
        /// <returns></returns>
        public List<GamePiece> MovableGamePieces(Player player, int dice)
        {
            List<GamePiece> movablePieces = new List<GamePiece>();

            if (dice == 6 && player.GamePiece.Any(gp => gp.IsAtBase))
            {
                movablePieces.AddRange(player.GamePiece.Where(gp => gp.IsAtBase));
            }
            movablePieces.AddRange(player.GamePiece.Where(gp => !gp.IsAtBase && !gp.IsAtGoal));

            return movablePieces;
        }

        /// <summary>
        /// Return a gamepiece from players list of gamepieces.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public GamePiece SelectGamePiece(Player player, int index)
        {
            return Session.SelectGamePiece(player, index);
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
