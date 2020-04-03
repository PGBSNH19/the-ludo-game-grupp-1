using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace EngineClasses
{
    public class GamePiece
    {
        [Key]
        public int GamePieceId { get; private set; }        
        public int YCoord { get; set; }
        public int XCoord { get; set; }
        public bool IsAtBase { get; set; }
        public bool IsAtGoal { get; set; }

        //Relationships
        public int PlayerId { get; private set; }
        public Player Player { get; private set; }

        public GamePiece(bool isAtBase, bool isAtGoal)
        {
            this.IsAtBase = isAtBase;
            this.IsAtGoal = isAtGoal;
        }

        public void AddToDb()
        {
            using (var context = new LudoContext())
            {
                //If exists do update instead
                if (context.GamePiece.Any(gp => gp.GamePieceId == this.GamePieceId))
                {
                    context.GamePiece.Update(this);
                }
                else
                {
                    context.GamePiece.Add(this);
                }
                
                context.SaveChanges();
            }
        }

        public void RemoveFromDb()
        {
            using (var context = new LudoContext())
            {
                if (context.GamePiece.Any(gp => gp.GamePieceId == this.GamePieceId))
                {
                    context.GamePiece.Remove(this);
                }

                context.SaveChanges();
            }
        }
    }
}
