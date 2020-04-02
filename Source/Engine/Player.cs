using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace EngineClasses
{
    public class Player
    {
        [Key]
        public int PlayerId { get; private set; }
        public string UserName { get; private set; }
        public string Color { get; private set; }

        //Relationships
        public int SessionId { get; private set; }
        public Session Session { get; private set; }
        public ICollection<GamePiece> GamePiece { get; private set; }

        public Player(string userName)
        {
            this.UserName = userName;
            GamePiece = new List<GamePiece>();
        }


        public int RollDice()
        {
            int result;
            Random rnd = new Random();

            result = rnd.Next(1, 6 + 1);
            return result;
        }

        public GamePiece SelectGamePiece(int index)
        {
            return GamePiece.ToList()[index];
        }

        public void MoveGamePiece(GamePiece gamePiece, int x, int y)
        {
            if(gamePiece.IsAtBase)
            {
                
            }
            gamePiece.UpdatePosition(x, y);
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
