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
        public ICollection<Player> Player { get; private set; }

        public Session()
        {
            Player = new List<Player>();
        }

        public void CreatePlayer(string userName)
        {
            Player player = new Player(userName);
            if (Player.Count < 4)
            {
                this.Player.Add(player);
            }
        }

        public Player CurrentPlayerTurn()
        {
            return Player.ToList()[(Turns % Player.Count) - 1];
        }

        //public GamePiece SelectGamePiece(Player player, int pieceNum)
        //{
        //    player.
        //}

        //public void MoveGamePiece(Player player, int x, int y)
        //{
        //    player.UpdateGamePiecePosition(x, y);
        //}

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
