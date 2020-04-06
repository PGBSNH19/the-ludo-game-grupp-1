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

        public List<GameSquare> AddPlayerSection(string color)
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

        public List<GameSquare> AddSharedSection()
        {
            List<GameSquare> section = new List<GameSquare>();
            for (int i = 0; i < 9; i++)
            {
                section.Add(new GameSquare(this.BoardRoute.Count + section.Count, "white", false, false));
            }

            return section;
        }

        public GameSquare ValidateStartingSquare(Player player)
        {
            return BoardRoute.Where(b => b.Color == player.Color && b.StartingSquare).FirstOrDefault();
        }

        public GameSquare ValidateCurrentSquare(GamePiece gamePiece)
        {
            return BoardRoute.Where(b => b.GameSquareNumber == gamePiece.Position).FirstOrDefault();
        }

        public GameSquare ValidateNextSquare(GamePiece gamePiece)
        {
            return BoardRoute.Where(b => b.GameSquareNumber == gamePiece.Position + 1).FirstOrDefault();
        }

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
