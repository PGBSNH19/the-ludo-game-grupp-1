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
                    BoardSquare startSquare = GameBoard.GetStartingSquare(gamePiece.Player);
                    gamePiece.BoardSquareNumber = startSquare.BoardSquareNumber;
                    startSquare.PlaceGamePiece(gamePiece);
                    gamePiece.IsAtBase = false;
                }
                else
                {
                    GameBoard.GetCurrentSquare(gamePiece).GamePieces.Remove(gamePiece);

                    for (int i = 0; i < steps; i++)
                    {                        
                        gamePiece.BoardSquareNumber = GameBoard.FindNextValidSquare(gamePiece).BoardSquareNumber;

                        //if (i == steps - 1 && GameBoard.GetNextSquare(gamePiece).EndSquare)
                        //{
                        //    gamePiece.IsAtGoal = true;
                        //}
                    }

                    BoardSquare currentSquare = GameBoard.GetCurrentSquare(gamePiece);              
                    currentSquare.PlaceGamePiece(gamePiece);
                }
            }
        }

        /// <summary>
        /// Returns bool based on if all players game pieces have entered their goal.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public bool IsWinner(Player player)
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

        /// <summary>
        /// Returns all GamePieces in session
        /// </summary>
        /// <returns></returns>
        public List<GamePiece> GetAllGamePieces()
        {
            List<GamePiece> gamePieces = new List<GamePiece>();
            foreach (Player player in Session.Player)
            {
                gamePieces.AddRange(player.GamePiece);
            }

            return gamePieces;
        }

        public Session LoadSession()
        {
            return Session.LoadSessionAsync().Result;
        }

        public void PlayCurrentSession()
        {
            this.Session = LoadSession();
            PlaceGamePieces(Session.Player);
        }

        public void PlaceGamePieces(List<Player> players)
        {
            for(int i = 0; i < players.Count; i++)
            {
                for(int j = 0; j < players[i].GamePiece.Count; j++)
                {
                    GameBoard.PlaceGamePiece(players[i].GamePiece[j]);
                }
            }
        }
    }
}
