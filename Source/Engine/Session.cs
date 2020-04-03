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
            if (Player.Count < 4)
            {
                Player player = new Player(userName);
                CreateGamePieces(player);
                this.Player.Add(player);
            }
        }

        private void CreateGamePieces(Player player)
        {
            for (int i = 0; i < 4; i++)
            {
                player.GamePiece.Add(new GamePiece(true, false));
            }
        }

        public Player CurrentPlayerTurn()
        {
            return Player.ToList()[(Turns % Player.Count)];
        }

        public void MoveGamePiece(Player player, GamePiece gamePiece, int xcoord, int ycoord)
        {
            //player.MoveGamePiece
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
