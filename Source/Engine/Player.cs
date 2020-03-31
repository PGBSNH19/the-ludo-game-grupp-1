using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EngineClasses
{
    public class Player
    {
        [Key]
        public int PlayerID { get; set; }
        public string UserName { get; set; }
        [ForeignKey("SessionID")]
        public int SessionID { get; set; }
        public Session Session { get; set; }
    }

}
