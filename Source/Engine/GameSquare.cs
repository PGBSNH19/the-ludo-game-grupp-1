using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace EngineClasses
{
    public class GameSquare
    {
       [Key]
        public int GameSquareId { get; set; } 

        public int GameSquareNumber { get; set; }
        public string Color { get; set; }
        public bool StartingSquare { get; set; }
        public bool EndSquare { get; set; }
        

        public GameSquare(int gameSquareNumber, string color, bool startingSquare, bool endSquare)
        {
            this.GameSquareNumber = gameSquareNumber;
            this.Color = color;
            this.StartingSquare = startingSquare;
            this.EndSquare = endSquare;
        }

    }
}
