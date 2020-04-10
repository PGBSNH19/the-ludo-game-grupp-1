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
        private LudoContext context;
        public Session Session { get; private set; }
        public GameBoard GameBoard { get; private set; }
        private GameLog gameLog;

        public GameEngine(Session session, GameBoard gameBoard, GameLog gameLog, LudoContext context)
        {
            this.Session = session;
            this.GameBoard = gameBoard;
            this.gameLog = gameLog;
            this.context = context;
        }

        /// <summary>
        /// Create a new player and add it to session.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="color"></param>
        public void CreatePlayer(string userName, string color) => Session.CreatePlayer(userName, color);


        /// <summary>
        /// Select player based on index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Player PlayerSelect(int index) => Session.Player[index];


        /// <summary>
        /// Returns current player whos turn it is.
        /// </summary>
        /// <returns></returns>
        public Player CurrentPlayer() => Session.GetCurrentPlayer();


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

                        if (GameBoard.Board[gamePiece.BoardSquareNumber.Value].EndSquare)
                        {
                            gamePiece.IsAtGoal = true;
                        }
                    }

                    if (!gamePiece.IsAtGoal)
                    {
                        BoardSquare currentSquare = GameBoard.GetCurrentSquare(gamePiece);
                        currentSquare.PlaceGamePiece(gamePiece);
                    }
                }
            }
        }

        /// <summary>
        /// Returns bool based on if all players game pieces have entered their goal.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public bool IsWinner(Player player) => player.GamePiece.All(gp => gp.IsAtGoal == true);

        public void CreateGameLog(string userName)
        {
            gameLog.CreateNewGameLog(userName);
            gameLog.AddToDb(context);
        }

        /// <summary>
        /// Randomize a number between 1 - 6.
        /// </summary>
        /// <returns></returns>
        public int RollDice() => Session.RollDice();

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

        public Session LoadSession() => Session.LoadSessionAsync(context).Result;
        public void SaveSession() => Session.AddToDb(context);
        public void PlayCurrentSession()
        {
            this.Session = LoadSession();
            PlaceGamePieces(Session.Player);
        }

        public void PlaceGamePieces(List<Player> players)
        {
            for (int i = 0; i < players.Count; i++)
            {
                for (int j = 0; j < players[i].GamePiece.Count; j++)
                {
                    GameBoard.PlaceGamePiece(players[i].GamePiece[j]);
                }
            }
        }
    }
}
