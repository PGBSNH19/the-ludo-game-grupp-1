﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace EngineClasses
{
    public class Session
    {
        [Key]
        public int SessionId { get; private set; }
        public ICollection<Player> Player { get; private set; }
        public int Turns { get; set; }

        public Session()
        {

        }

        public void AddToDb()
        {
            using (var context = new LudoContext())
            {
                //If exists do update instead
                if (context.Session.Any(s => s.SessionId == this.SessionId))
                {
                    context.Session.Update(this);
                }
                else
                {
                    context.Session.Add(this);
                }

                context.SaveChanges();
            }
        }

        public void RemoveFromDb()
        {
            using (var context = new LudoContext())
            {
                if (context.Session.Any(s => s.SessionId == this.SessionId))
                {
                    context.Session.Remove(this);
                }

                context.SaveChanges();
            }
        }
    }
}
