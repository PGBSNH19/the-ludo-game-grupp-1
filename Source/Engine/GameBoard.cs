using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EngineClasses
{
    public class GameBoard
    {
        public List<BoardSquare> Board { get; set; }
        public GameBoard()
        {
            this.Board = new List<BoardSquare>();

            this.Board.AddRange(AddPlayerSection("Red"));
            this.Board.AddRange(AddSharedSection());

            this.Board.AddRange(AddPlayerSection("Blue"));
            this.Board.AddRange(AddSharedSection());

            this.Board.AddRange(AddPlayerSection("Yellow"));
            this.Board.AddRange(AddSharedSection());

            this.Board.AddRange(AddPlayerSection("Green"));
            this.Board.AddRange(AddSharedSection());
        }

        private List<BoardSquare> AddPlayerSection(string color)
        {
            List<BoardSquare> section = new List<BoardSquare>();

            for (int i = 0; i < 4; i++)
            {
                section.Add(new BoardSquare(this.Board.Count + section.Count, color, false, false));
            }
            section.Add(new BoardSquare(this.Board.Count + section.Count, color, false, true));
            section.Add(new BoardSquare(this.Board.Count + section.Count, color, true, false));

            return section;
        }

        private List<BoardSquare> AddSharedSection()
        {
            List<BoardSquare> section = new List<BoardSquare>();
            for (int i = 0; i < 9; i++)
            {
                section.Add(new BoardSquare(this.Board.Count + section.Count, "White", false, false));
            }

            return section;
        }

        /// <summary>
        /// Places gamePiece in squares' list of game pieces.
        /// </summary>
        /// <param name="gamePiece"></param>
        public void AddGamePiecesToBoard(GamePiece gamePiece)
        {
            if (Board.Where(bs => bs.BoardSquareNumber == gamePiece.BoardSquareNumber).Any())
            {
                BoardSquare boardSquare = Board.Where(bs => bs.BoardSquareNumber == gamePiece.BoardSquareNumber.Value).FirstOrDefault();
                boardSquare.GamePieces.Add(gamePiece);
            }
        }

        /// <summary>
        /// Returns the next square the game piece can legally stand on according to game rules.
        /// </summary>
        /// <param name="gamePiece"></param>
        /// <returns></returns>
        public BoardSquare FindNextValidSquare(GamePiece gamePiece)
        {
            int nextSquareNumber;
            if (gamePiece.BoardSquareNumber >= this.Board.Count - 1)
            {
                nextSquareNumber = 0;
            }
            else
            {
                nextSquareNumber = gamePiece.BoardSquareNumber.Value + 1;
            }

            int nextValidSquareNumber = FindNextValidBoardSquareNumber(gamePiece, nextSquareNumber);

            return Board[nextValidSquareNumber];
        }
        /// <summary>
        /// Loops through board and returns next valid squareNumber.
        /// </summary>
        /// <param name="gamePiece"></param>
        /// <param name="nextSquareNumber"></param>
        /// <returns></returns>
        private int FindNextValidBoardSquareNumber(GamePiece gamePiece, int nextSquareNumber)
        {
            while (this.Board[nextSquareNumber].Color != gamePiece.Player.Color &&
                    this.Board[nextSquareNumber].Color != "White")
            {
                nextSquareNumber++;

                if (nextSquareNumber >= this.Board.Count)
                {
                    nextSquareNumber = 0;
                }
            }
            return nextSquareNumber;
        }

        /// <summary>
        /// Return starting square based on player color.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public BoardSquare GetStartingSquare(Player player) => Board.Where(b => b.Color == player.Color && b.IsStartingSquare).FirstOrDefault();

        /// <summary>
        /// Returns gamesquare based on game pieces position (int).
        /// </summary>
        /// <param name="gamePiece"></param>
        /// <returns></returns>
        public BoardSquare GetCurrentSquare(GamePiece gamePiece) => Board.Where(b => b.BoardSquareNumber == gamePiece.BoardSquareNumber.Value).FirstOrDefault();

        public void PlaceGamePiece(GamePiece gamePiece, BoardSquare boardSquare)
        {
            BoardSquare currentSquare = this.Board.Where(bs => bs == boardSquare).FirstOrDefault();
            currentSquare.PlaceGamePiece(gamePiece);
        }
    }
}
