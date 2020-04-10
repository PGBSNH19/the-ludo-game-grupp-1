using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace EngineClasses
{
    public class GameLog
    {
        [Key]
        public int GameLogId { get; private set; }
        public string WinnerPlayer { get; private set; }
        public DateTime Created { get; set; }

        public GameLog(string winnerPlayer)
        {
            this.WinnerPlayer = winnerPlayer;
            this.Created = DateTime.UtcNow;
        }

        public GameLog()
        {
            this.Created = DateTime.UtcNow;
        }

        public void CreateNewGameLog(string userName)
        {
            this.WinnerPlayer = userName;
        }
        public void AddToDb(LudoContext context)
        {
            context = new LudoContext();

            //If exists do update instead
            if (context.GameLog.Any(gl => gl.GameLogId == this.GameLogId))
            {
                context.GameLog.Update(this);
            }
            else
            {
                context.GameLog.Add(this);
            }

            context.SaveChanges();

        }
    }
}
