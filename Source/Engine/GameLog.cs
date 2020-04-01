using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EngineClasses
{
    public class GameLog
    {
        [Key]
        public int GameLogId { get; set; }
        public string WinnerPlayer{ get; set; }
        public DateTime Created { get; set; }
    }
}
