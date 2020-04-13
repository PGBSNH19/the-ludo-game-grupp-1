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

        public GamePiece() { }

        public GamePiece(Player player, int gamePieceNumber)
        {
            this.Player = player;
            this.GamePieceNumber = gamePieceNumber;
            this.IsAtBase = true;
            this.IsAtGoal = false;
        }


        public void SetAtBase()
        {
            this.IsAtBase = true;
            this.BoardSquareNumber = null;
        }
    }
}
