using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EngineClasses
{
    public class GamePiece
    {
        [Key]
        public int GamePieceID { get; set; }
        [ForeignKey("PlayerID")]
        public int PlayerID { get; set; }
        public Player Player { get; set; }
        public string Color { get; set; }
        public int YCoord { get; set; }
        public int XCoord { get; set; }
    }
}
