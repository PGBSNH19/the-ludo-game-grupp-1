using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EngineClasses
{
    public class GameBoard
    {
        public string[,] Placements { get; private set; } = new string[11, 11];
        public Dictionary<string, int> Bases { get; private set; }
        public List<GameSquare> BoardRoute { get; set; }
        public GameBoard()
        {
            this.BoardRoute.AddRange(AddPlayerSection("Red"));
            this.BoardRoute.AddRange(AddSharedSection());

            this.BoardRoute.AddRange(AddPlayerSection("Blue"));
            this.BoardRoute.AddRange(AddSharedSection());

            this.BoardRoute.AddRange(AddPlayerSection("Yellow"));
            this.BoardRoute.AddRange(AddSharedSection());

            this.BoardRoute.AddRange(AddPlayerSection("Green"));
            this.BoardRoute.AddRange(AddSharedSection());

            AddToDb(this.BoardRoute);

            Placements = new string[,] {
                { "x","x","x","x","a","a","a","x","x","x","x"},
                { "x","x","x","x","a","r","a","x","x","x","x"},
                { "x","x","x","x","a","r","a","x","x","x","x"},
                { "x","x","x","x","a","r","a","x","x","x","x"},
                { "a","a","a","a","a","r","a","a","a","a","a"},
                { "a","b","b","b","b","G","y","y","y","y","a"},
                { "a","a","a","a","a","g","a","a","a","a","a"},
                { "x","x","x","x","a","g","a","x","x","x","x"},
                { "x","x","x","x","a","g","a","x","x","x","x"},
                { "x","x","x","x","a","g","a","x","x","x","x"},
                { "x","x","x","x","a","a","a","x","x","x","x"}
            };
        }

        public List<GameSquare> AddPlayerSection(string color)
        {
            List<GameSquare> section = new List<GameSquare>();
            section.Add(new GameSquare(color, true, false));
            for (int i = 0; i < 4; i++)
            {
                section.Add(new GameSquare(color, false, false));
            }
            section.Add(new GameSquare(color, false, true));

            return section;
        }

        public List<GameSquare> AddSharedSection()
        {
            List<GameSquare> section = new List<GameSquare>();
            for (int i = 0; i < 9; i++)
            {
                section.Add(new GameSquare("white", false, false));
            }

            return section;
        }


        private List<GameSquare> AddGameSquares()
        {
            List<GameSquare> boardRoute = new List<GameSquare>();
            bool StartingSquare = true;
            bool EndSquare = true;

            boardRoute.Add(new GameSquare("green", StartingSquare, false));
            boardRoute.Add(new GameSquare("green", false, false));
            boardRoute.Add(new GameSquare("green", false, false));
            boardRoute.Add(new GameSquare("green", false, false));
            boardRoute.Add(new GameSquare("green", false, false));
            boardRoute.Add(new GameSquare("green", false, EndSquare));

            boardRoute.Add(new GameSquare("white", false, false));
            boardRoute.Add(new GameSquare("white", false, false));
            boardRoute.Add(new GameSquare("white", false, false));
            boardRoute.Add(new GameSquare("white", false, false));
            boardRoute.Add(new GameSquare("white", false, false));
            boardRoute.Add(new GameSquare("white", false, false));
            boardRoute.Add(new GameSquare("white", false, false));
            boardRoute.Add(new GameSquare("white", false, false));
            boardRoute.Add(new GameSquare("white", false, false));

            boardRoute.Add(new GameSquare("blue", false, false));
            boardRoute.Add(new GameSquare("blue", false, false));
            boardRoute.Add(new GameSquare("blue", false, false));
            boardRoute.Add(new GameSquare("blue", false, false));
            boardRoute.Add(new GameSquare("blue", false, EndSquare));
            boardRoute.Add(new GameSquare("blue", StartingSquare, true));

            boardRoute.Add(new GameSquare("white", false, false));
            boardRoute.Add(new GameSquare("white", false, false));
            boardRoute.Add(new GameSquare("white", false, false));
            boardRoute.Add(new GameSquare("white", false, false));
            boardRoute.Add(new GameSquare("white", false, false));
            boardRoute.Add(new GameSquare("white", false, false));
            boardRoute.Add(new GameSquare("white", false, false));
            boardRoute.Add(new GameSquare("white", false, false));
            boardRoute.Add(new GameSquare("white", false, false));

            boardRoute.Add(new GameSquare("red", false, false));
            boardRoute.Add(new GameSquare("red", false, false));
            boardRoute.Add(new GameSquare("red", false, false));
            boardRoute.Add(new GameSquare("red", false, false));
            boardRoute.Add(new GameSquare("red", false, EndSquare));
            boardRoute.Add(new GameSquare("red", StartingSquare, true));

            boardRoute.Add(new GameSquare("white", false, false));
            boardRoute.Add(new GameSquare("white", false, false));
            boardRoute.Add(new GameSquare("white", false, false));
            boardRoute.Add(new GameSquare("white", false, false));
            boardRoute.Add(new GameSquare("white", false, false));
            boardRoute.Add(new GameSquare("white", false, false));
            boardRoute.Add(new GameSquare("white", false, false));
            boardRoute.Add(new GameSquare("white", false, false));
            boardRoute.Add(new GameSquare("white", false, false));

            boardRoute.Add(new GameSquare("yellow", false, false));
            boardRoute.Add(new GameSquare("yellow", false, false));
            boardRoute.Add(new GameSquare("yellow", false, false));
            boardRoute.Add(new GameSquare("yellow", false, false));
            boardRoute.Add(new GameSquare("yellow", false, EndSquare));
            boardRoute.Add(new GameSquare("yellow", StartingSquare, true));

            boardRoute.Add(new GameSquare("white", false, false));
            boardRoute.Add(new GameSquare("white", false, false));
            boardRoute.Add(new GameSquare("white", false, false));
            boardRoute.Add(new GameSquare("white", false, false));
            boardRoute.Add(new GameSquare("white", false, false));
            boardRoute.Add(new GameSquare("white", false, false));
            boardRoute.Add(new GameSquare("white", false, false));
            boardRoute.Add(new GameSquare("white", false, false));
            boardRoute.Add(new GameSquare("white", false, false));

            return boardRoute;
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

            GameSquare gs = ValidateStartingSquare(gamePiece);

                int index = gs.GameSquareId;
         

            for (int i = index; i < dice + index; i++)
            {
                if (BoardRoute[i + 1] == null)
                {
                    i = 0;
                }
                if (BoardRoute[i].Color != null)
                {
                    if (BoardRoute[i].Color == gamePiece.Player.Color)
                    {
                        gamePiece.GameSquareId = BoardRoute[i].GameSquareId;

                    }
                    else
                    {
                        i = index;
                    }
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
