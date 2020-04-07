using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EngineClasses
{
    public class GameSquare
    {
        public int GameSquareNumber { get; set; }
        public string Color { get; set; }
        public bool StartingSquare { get; set; }
        public bool EndSquare { get; set; }

        public List<GamePiece> GamePieces { get; private set; }
        

        public GameSquare(int gameSquareNumber, string color, bool startingSquare, bool endSquare)
        {
            this.GameSquareNumber = gameSquareNumber;
            this.Color = color;
            this.StartingSquare = startingSquare;
            this.EndSquare = endSquare;
            this.GamePieces = new List<GamePiece>();
        }

        public void AddGamePiece(GamePiece gamePiece)
        {
            GamePieces = GamePieces
                .Where(gp => gp.Player.Color != gamePiece.Player.Color)
                .Select(gp => { gp.IsAtBase = true; gp.Position = null; return gp; } )
                .ToList();
            GamePieces.RemoveAll(gp => gp.Player.Color != gamePiece.Player.Color);
        }

    }
}
