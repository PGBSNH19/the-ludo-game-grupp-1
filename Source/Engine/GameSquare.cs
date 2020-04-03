using System;
using System.Collections.Generic;
using System.Text;

namespace EngineClasses
{
    public class GameSquare
    {
        public int Index { get; set; }
        public string Color { get; set; }
        public bool StartingSquare { get; set; }
        public bool EndSquare { get; set; }

        public GameSquare(int index, string color, bool startingSquare, bool endSquare)
        {
            this.Index = index;
            this.Color = color;
            this.StartingSquare = startingSquare;
            this.EndSquare = endSquare;
        }

    }
}
