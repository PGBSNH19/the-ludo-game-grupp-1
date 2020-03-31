using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Engine
{
    public class Session
    {
        [Key]
        public int SessionID { get; set; }
        [ForeignKey("TurnPlayerID")]
        public int TurnPlayerID { get; set; }
        public Player TurnPlayer { get; set; }
        public int Turns { get; set; }
    }
}
