using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineClasses
{
    public class GameEngine
    {
        public Session Session { get; private set; }
        public GameBoard GameBoard { get; private set; }
        private GameLog gameLog;
        private LudoContext context;

        public GameEngine(Session session, GameBoard gameBoard, GameLog gameLog, LudoContext context)
        {
            this.Session = session;
            this.GameBoard = gameBoard;
            this.gameLog = gameLog;
            this.context = context;
        }

        /// <summary>
        /// Add gamepieces to board when sessions is loaded from database.
        /// </summary>
        /// <param name="players"></param>
        public void AddGamePiecesToBoard(List<Player> players)
        {
            for (int i = 0; i < players.Count; i++)
            {
                for (int j = 0; j < players[i].GamePieces.Count; j++)
                {
                    GameBoard.AddGamePiecesToBoard(players[i].GamePieces[j]);
                }
            }
        }
        /// <summary>
        /// Creates a gamelog and adds it to the database.
        /// </summary>
        /// <param name="userName"></param>
        public async void CreateGameLog(string userName)
        {
            gameLog.CreateNewGameLog(userName);
            await gameLog.AddToDb(context);
        }

        /// <summary>
        /// Create a new player and add it to session.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="color"></param>
        public void CreatePlayer(string userName, string color) => Session.CreatePlayer(userName, color);

        /// <summary>
        /// Returns current player whos turn it is.
        /// </summary>
        /// <returns></returns>
        public Player CurrentPlayer() => Session.GetCurrentPlayer();

        
        /// <summary>
        /// Returns bool based on if all players game pieces have entered their goal.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public bool IsWinner(Player player) => player.GamePieces.All(gp => gp.IsAtGoal == true);

        public Session LoadSession() => Session.LoadSessionAsync(context).Result;
        /// <summary>
        /// Return a list specifying wich game pieces a player have that can legally move.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="dice"></param>
        /// <returns></returns>
        
        public List<GamePiece> MovableGamePieces(Player player, int dice)
        {
            List<GamePiece> movablePieces = new List<GamePiece>();

            if (dice == 6 && player.GamePieces.Any(gp => gp.IsAtBase))
            {
                movablePieces.AddRange(player.GamePieces.Where(gp => gp.IsAtBase));
            }
            movablePieces.AddRange(player.GamePieces.Where(gp => !gp.IsAtBase && !gp.IsAtGoal));

            return movablePieces;
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
                    GameBoard.PlaceGamePiece(gamePiece, startSquare);
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
                        GameBoard.PlaceGamePiece(gamePiece, currentSquare);
                    }
                }
            }
        }

        public void PlayCurrentSession()
        {
            this.Session = LoadSession();
            AddGamePiecesToBoard(Session.Player);
        }

        /// <summary>
        /// Select player based on index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// 
        public Player PlayerSelect(int index) => Session.Player[index];

        public void RemoveSession() => Session.RemoveFromDb(context);

        /// <summary>
        /// Randomize a number between 1 - 6.
        /// </summary>
        /// <returns></returns>
        public int RollDice() => Session.RollDice();

        public async void SaveSession() => await Session.AddToDbAsync(context);

    }
}
