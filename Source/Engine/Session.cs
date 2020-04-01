using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EngineClasses
{
    public class Session
    {
        [Key]
        public int SessionId { get; set; }
        public ICollection<Player> Player { get; set; }
        public int Turns { get; set; }
    }
}
