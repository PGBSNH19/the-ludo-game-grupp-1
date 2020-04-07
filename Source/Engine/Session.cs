using Microsoft.EntityFrameworkCore;
using System;
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
        public int Turns { get; set; }

        //Relationships
        public List<Player> Player { get; private set; }

        public Session()
        {
            this.Player = new List<Player>();
            this.Turns = 0;
        }

        public void CreatePlayer(string userName, string color)
        {
            if (Player.Count < 4)
            {
                Player player = new Player(userName, color);
                this.Player.Add(player);
            }
        }        

        public Player GetCurrentPlayer()
        {
            return Player.ToList()[(Turns % Player.Count)];
        }

        public GamePiece SelectGamePiece(Player player, int index)
        {
            return player.SelectGamePiece(index);
        }

        public int RollDice()
        {
            int result;
            Random rnd = new Random();

            result = rnd.Next(1, 6 + 1);
            return result;
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
