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

        public GameSquare ValidateStartingSquare(GamePiece gamePiece)
        {

            return BoardRoute.Where(b => b.Color == gamePiece.Player.Color && b.StartingSquare).FirstOrDefault();
        }

        public GameSquare ValidateCurrentSquare(GamePiece gamePiece)
        {
            return BoardRoute.Where(b => b.GameSquareId == gamePiece.GameSquareId).FirstOrDefault();
        }

        public void ContinueRoute(GamePiece gamePiece, int dice)
        {
            if (gamePiece.IsAtBase && dice < 6)
            {
                return;
            }

            GameSquare gs = ValidateStartingSquare(gamePiece);

            for (int i = 0; i < dice; i++)
            {
                if (gs.GameSquareId + 1 >= BoardRoute.Count)
                {
                    gamePiece.GameSquare = BoardRoute[0];
                    gamePiece.GameSquareId = BoardRoute[0].GameSquareId;
                }
                else
                {
                    gamePiece.GameSquare = BoardRoute[ValidateCurrentSquare(gamePiece).GameSquareId + 1];
                    gamePiece.GameSquareId = BoardRoute[ValidateCurrentSquare(gamePiece).GameSquareId + 1].GameSquareId;
                }
            }
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
