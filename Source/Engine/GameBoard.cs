using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EngineClasses
{
    public class GameBoard
    {
        public Dictionary<string, int> Bases { get; private set; }
        public List<GameSquare> BoardRoute { get; set; }
        public GameBoard()
        {
            this.BoardRoute = new List<GameSquare>();

            this.BoardRoute.AddRange(AddPlayerSection("Red"));
            this.BoardRoute.AddRange(AddSharedSection());

            this.BoardRoute.AddRange(AddPlayerSection("Blue"));
            this.BoardRoute.AddRange(AddSharedSection());

            this.BoardRoute.AddRange(AddPlayerSection("Yellow"));
            this.BoardRoute.AddRange(AddSharedSection());

            this.BoardRoute.AddRange(AddPlayerSection("Green"));
            this.BoardRoute.AddRange(AddSharedSection());
        }

        private List<GameSquare> AddPlayerSection(string color)
        {
            List<GameSquare> section = new List<GameSquare>();
            section.Add(new GameSquare(this.BoardRoute.Count + section.Count, color, true, false));
            for (int i = 0; i < 4; i++)
            {
                section.Add(new GameSquare(this.BoardRoute.Count + section.Count, color, false, false));
            }
            section.Add(new GameSquare(this.BoardRoute.Count + section.Count, color, false, true));

            return section;
        }

        private List<GameSquare> AddSharedSection()
        {
            List<GameSquare> section = new List<GameSquare>();
            for (int i = 0; i < 9; i++)
            {
                section.Add(new GameSquare(this.BoardRoute.Count + section.Count, "White", false, false));
            }

            return section;
        }

        /// <summary>
        /// Return starting square based on player color.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public GameSquare GetStartingSquare(Player player)
        {
            return BoardRoute.Where(b => b.Color == player.Color && b.StartingSquare).FirstOrDefault();
        }

        /// <summary>
        /// Returns gamesquare based on game pieces position (int).
        /// </summary>
        /// <param name="gamePiece"></param>
        /// <returns></returns>
        public GameSquare GetCurrentSquare(GamePiece gamePiece)
        {
            return BoardRoute.Where(b => b.GameSquareNumber == gamePiece.Position).FirstOrDefault();
        }

        /// <summary>
        /// Returns the square after the square the game piece is currently at.
        /// </summary>
        /// <param name="gamePiece"></param>
        /// <returns></returns>
        public GameSquare GetNextSquare(GamePiece gamePiece)
        {
            return BoardRoute.Where(b => b.GameSquareNumber == gamePiece.Position + 1).FirstOrDefault();
        }

        /// <summary>
        /// Returns the next square the game piece can legally stand on according to game rules.
        /// </summary>
        /// <param name="gamePiece"></param>
        /// <returns></returns>
        public GameSquare FindNextValidSquare(GamePiece gamePiece)
        {
            int i = 1;

            while (BoardRoute[gamePiece.Position.Value + i].Color != gamePiece.Player.Color ||
                    BoardRoute[gamePiece.Position.Value + i].Color != "White")
            {
                i++;
            }

            return BoardRoute[gamePiece.Position.Value + i];
        }
        
        public void AddToDb(List<GameSquare> gameSquares)
        {
            using (var context = new LudoContext())
            {
                foreach (var s in gameSquares)
                {
                    context.GameSquare.Add(s);
                }

                context.SaveChanges();
            }
        }
    }
}
