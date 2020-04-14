using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineClasses
{
    public class GameLog
    {
        [Key]
        public int GameLogId { get; private set; }
        public string WinnerPlayer { get; private set; }
        public DateTime Created { get; private set; }

        public void CreateNewGameLog(string userName)
        {
            this.WinnerPlayer = userName;
            this.Created = DateTime.UtcNow;

        }

        public async Task AddToDb(LudoContext context)
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

            await context.SaveChangesAsync();

        }
    }
}
