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
        public List<GamePiece> GamePiece { get; private set; }


        public Player(string userName, string color)
        {
            this.UserName = userName;
            this.Color = color;

            this.GamePiece = new List<GamePiece>();
            
        }

        public void AddGamePieces()
        {
            if (GamePiece.Count <= 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    this.GamePiece.Add(new GamePiece(this, i + 1));
                }
            }
        }

        public GamePiece SelectGamePiece(int index)
        {
            return GamePiece[index];
        }
    }
}
