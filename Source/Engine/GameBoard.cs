using System;
using System.Collections.Generic;
using System.Linq;

namespace EngineClasses
{
    public class GameBoard
    {
        public string[,] Placements { get; private set; } = new string[11, 11];
        public Dictionary<string, int> Bases { get; private set; }
        public List<GameSquare> BoardRoute { get; set; }
        public GameBoard()
        {
            this.BoardRoute = AddGameSquares();




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
        private List<GameSquare> AddGameSquares()
        {
            List<GameSquare> boardRoute = new List<GameSquare>();
            bool StartingSquare = true;
            bool EndSquare = true;

            boardRoute.Add(new GameSquare(6, "green", StartingSquare, false));
            boardRoute.Add(new GameSquare(1, "green", false, false));
            boardRoute.Add(new GameSquare(2, "green", false, false));
            boardRoute.Add(new GameSquare(3, "green", false, false));
            boardRoute.Add(new GameSquare(4, "green", false, false));
            boardRoute.Add(new GameSquare(5, "green", false, EndSquare));

            boardRoute.Add(new GameSquare(7, "white", false, false));
            boardRoute.Add(new GameSquare(8, "white", false, false));
            boardRoute.Add(new GameSquare(9, "white", false, false));
            boardRoute.Add(new GameSquare(10, "white", false, false));
            boardRoute.Add(new GameSquare(11, "white", false, false));
            boardRoute.Add(new GameSquare(12, "white", false, false));
            boardRoute.Add(new GameSquare(8, "white", false, false));
            boardRoute.Add(new GameSquare(9, "white", false, false));
            boardRoute.Add(new GameSquare(10, "white", false, false));

            boardRoute.Add(new GameSquare(11, "blue", false, false));
            boardRoute.Add(new GameSquare(12, "blue", false, false));
            boardRoute.Add(new GameSquare(13, "blue", false, false));
            boardRoute.Add(new GameSquare(14, "blue", false, false));
            boardRoute.Add(new GameSquare(15, "blue", false, EndSquare));
            boardRoute.Add(new GameSquare(15, "blue", StartingSquare, true));

            boardRoute.Add(new GameSquare(2, "white", false, false));
            boardRoute.Add(new GameSquare(3, "white", false, false));
            boardRoute.Add(new GameSquare(4, "white", false, false));
            boardRoute.Add(new GameSquare(5, "white", false, false));
            boardRoute.Add(new GameSquare(6, "white", false, false));
            boardRoute.Add(new GameSquare(7, "white", false, false));
            boardRoute.Add(new GameSquare(8, "white", false, false));
            boardRoute.Add(new GameSquare(9, "white", false, false));
            boardRoute.Add(new GameSquare(10, "white", false, false));

            boardRoute.Add(new GameSquare(11, "red", false, false));
            boardRoute.Add(new GameSquare(12, "red", false, false));
            boardRoute.Add(new GameSquare(13, "red", false, false));
            boardRoute.Add(new GameSquare(14, "red", false, false));
            boardRoute.Add(new GameSquare(15, "red", false, EndSquare));
            boardRoute.Add(new GameSquare(15, "red", StartingSquare, true));

            boardRoute.Add(new GameSquare(2, "white", false, false));
            boardRoute.Add(new GameSquare(3, "white", false, false));
            boardRoute.Add(new GameSquare(4, "white", false, false));
            boardRoute.Add(new GameSquare(5, "white", false, false));
            boardRoute.Add(new GameSquare(6, "white", false, false));
            boardRoute.Add(new GameSquare(7, "white", false, false));
            boardRoute.Add(new GameSquare(8, "white", false, false));
            boardRoute.Add(new GameSquare(9, "white", false, false));
            boardRoute.Add(new GameSquare(10, "white", false, false));

            boardRoute.Add(new GameSquare(11, "yellow", false, false));
            boardRoute.Add(new GameSquare(12, "yellow", false, false));
            boardRoute.Add(new GameSquare(13, "yellow", false, false));
            boardRoute.Add(new GameSquare(14, "yellow", false, false));
            boardRoute.Add(new GameSquare(15, "yellow", false, EndSquare));
            boardRoute.Add(new GameSquare(15, "yellow", StartingSquare, true));

            boardRoute.Add(new GameSquare(2, "white", false, false));
            boardRoute.Add(new GameSquare(3, "white", false, false));
            boardRoute.Add(new GameSquare(4, "white", false, false));
            boardRoute.Add(new GameSquare(5, "white", false, false));
            boardRoute.Add(new GameSquare(6, "white", false, false));
            boardRoute.Add(new GameSquare(7, "white", false, false));
            boardRoute.Add(new GameSquare(8, "white", false, false));
            boardRoute.Add(new GameSquare(9, "white", false, false));
            boardRoute.Add(new GameSquare(10, "white", false, false));

            return boardRoute;
        }
        public GameSquare ValidateStartingSquare(GamePiece gamePiece)
        {

            return StartingSquares.Where(g => g.Color == gamePiece.Player.Color).FirstOrDefault();
        }
        
        public GameSquare ValidateCurrentSquare(GamePiece gamePiece)
        {
            return BoardRoute.Where(b => b.XCoordinate == gamePiece.XCoord && b.YCoordinate == gamePiece.YCoord).First();
        }

        public void ContinueRoute(GamePiece gamePiece, int dice)
        {
            GameSquare gs = ValidateCurrentSquare(gamePiece);
            int index = gs.Index;

            for (int i = index; i < dice + index; i++)
            {
                if(BoardRoute[i + 1] == null)
                {
                    i = 0;
                }
                if (BoardRoute[i].Color != null)
                {
                    if(BoardRoute[i].Color == gamePiece.Player.Color)
                    {
                        gamePiece.XCoord = BoardRoute[i].XCoordinate;
                        gamePiece.YCoord = BoardRoute[i].YCoordinate;
                    }
                    else
                    {
                        i = index;
                    }
                }
                
            }
            

        }
    }
}
