using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Engine
{
    public class GameLog
    {
        [Key]
        public int GameLogID { get; set; }
        [ForeignKey("WinnerPlayerID")]
        public int WinnerPlayerID { get; set; }
        public Player WinnerPlayer { get; set; }
        public DateTime Created { get; set; }
    }
}
