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
        public List<GamePiece> GamePieces { get; private set; }


        public Player(string userName, string color)
        {
            this.UserName = userName;
            this.Color = color;
            this.GamePieces = new List<GamePiece>();
        }

        public void AddGamePieces()
        {
            if (GamePieces.Count <= 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    this.GamePieces.Add(new GamePiece(this, i + 1));
                }
            }
        }

<<<<<<< HEAD
        public GamePiece SelectGamePiece(int index) => GamePieces[index];
=======
        public void RemoveGamePieces()
        {
            this.GamePiece = new List<GamePiece>();
        }

        public GamePiece SelectGamePiece(int index) => GamePiece[index];
>>>>>>> @Gerglamesh, started working on bugfixes where ending a running session and either loading a new session or creating a new session results in Players not being added correctly to session.
    }
}
