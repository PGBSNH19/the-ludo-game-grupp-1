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
        public int GamePieceNumber { get; set; }
        public bool IsAtBase { get; set; }
        public bool IsAtGoal { get; set; }
        

        //Relationships
        public int PlayerId { get; private set; }
        public Player Player { get; private set; }
        public int? BoardSquareNumber { get; set; }


        public GamePiece(int gamePieceNumber)
        {
            this.GamePieceNumber = gamePieceNumber;
            this.IsAtBase = true;
            this.IsAtGoal = false;
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
