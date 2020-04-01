﻿using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EngineClasses
{
    public class Player
    {
        [Key]
        public int PlayerId { get; private set; }
        public string UserName { get; private set; }

        public int SessionId { get; private set; }
        public Session Session { get; private set; }

        public Player()
        {

        }

        public void AddToDb()
        {
            using (var context = new LudoContext())
            {
                //If exists do update instead
                if (context.Player.Any(p => p.PlayerId == this.PlayerId))
                {
                    context.Player.Update(this);
                }
                else
                {
                    context.Player.Add(this);
                }

                context.SaveChanges();
            }
        }

        public void RemoveFromDb()
        {
            using (var context = new LudoContext())
            {
                if (context.Player.Any(p => p.PlayerId == this.PlayerId))
                {
                    context.Player.Remove(this);
                }

                context.SaveChanges();
            }
        }
    }
}
