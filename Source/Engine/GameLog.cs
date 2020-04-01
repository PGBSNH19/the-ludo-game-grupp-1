﻿using System;
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
        public string WinnerPlayer{ get; private set; }
        public DateTime Created { get; private set; }

        public GameLog()
        {

        }

        public void AddToDb()
        {
            using (var context = new LudoContext())
            {
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

        public void RemoveFromDb()
        {
            using (var context = new LudoContext())
            {
                if (context.GameLog.Any(gl => gl.GameLogId == this.GameLogId))
                {
                    context.GameLog.Remove(this);
                }

                context.SaveChanges();
            }
        }
    }
}
