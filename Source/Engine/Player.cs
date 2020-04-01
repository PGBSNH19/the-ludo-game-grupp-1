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
        public int PlayerId { get; set; }
        public string UserName { get; set; }

        public int SessionId { get; set; }
        public Session Session { get; set; }
    }

}
