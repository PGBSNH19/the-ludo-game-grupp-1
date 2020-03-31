using System;
using System.Collections.Generic;

namespace Engine
{
    public class GameBoard
    {
        public int[,] Placements { get; set; }
        public Dictionary<string, int> Bases { get; set; }
        public Dictionary<string, int> Goal { get; set; }
    }
}
