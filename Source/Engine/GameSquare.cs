using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace EngineClasses
{
    public class GameSquare
    {
       [Key]
        public int GameSquareID { get; set; } 

        public string Color { get; set; }
        public bool StartingSquare { get; set; }
        public bool EndSquare { get; set; }
        

        public GameSquare( string color, bool startingSquare, bool endSquare)
        {

            
            this.Color = color;
            this.StartingSquare = startingSquare;
            this.EndSquare = endSquare;
        }

    }
}
