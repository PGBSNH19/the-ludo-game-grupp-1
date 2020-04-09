using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EngineClasses
{
    public class BoardSquare
    {
        public int BoardSquareNumber { get; set; }
        public string Color { get; set; }
        public bool StartingSquare { get; set; }
        public bool EndSquare { get; set; }

        public List<GamePiece> GamePieces { get; private set; }

        public BoardSquare(int boardSquareNumber, string color, bool startingSquare, bool endSquare)
        {
            this.BoardSquareNumber = boardSquareNumber;
            this.Color = color;
            this.StartingSquare = startingSquare;
            this.EndSquare = endSquare;
            this.GamePieces = new List<GamePiece>();
        }

        /// <summary>
        /// Places gamePiece in squares' list of game pieces and sets all pieces of different colors to base.
        /// </summary>
        /// <param name="gamePiece"></param>
        public void PlaceGamePiece(GamePiece gamePiece)
        {
            GamePieces = GamePieces
                .Where(gp => gp.Player.Color != gamePiece.Player.Color)
                .Select(gp => { gp.IsAtBase = true; gp.BoardSquareNumber = null; return gp; })
                .ToList();
            GamePieces.RemoveAll(gp => gp.Player.Color != gamePiece.Player.Color);
            GamePieces.Add(gamePiece);
        }
    }
}
