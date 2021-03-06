﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineClasses
{
    public class Session
    {
        [Key]
        public int SessionId { get; private set; }
        public int Turns { get; set; }

        //Relationships
        public List<Player> Players { get; private set; }

        public Session()
        {
            this.Players = new List<Player>();
            this.Turns = 0;
        }

        public void CreatePlayer(string userName, string color)
        {
            if (Players.Count < 4)
            {
                Player player = new Player(userName, color);
                player.AddGamePieces();
                this.Players.Add(player);
            }
            else
            {
                throw new ArgumentOutOfRangeException("You cant add more than 4 players.");
            }
        }

        public Player GetCurrentPlayer() => Players[(Turns % Players.Count)];

        public int RollDice()
        {
            int result;
            Random rnd = new Random();

            result = rnd.Next(1, 6 + 1);
            return result;
        }

        public async Task<Session> LoadSessionAsync(LudoContext context)
        {
            Session session = null;
            context = new LudoContext();

            session = await context.Session
                    .Include(s => s.Players)
                    .ThenInclude(p => p.GamePieces)
                    .FirstOrDefaultAsync();
            context.SaveChanges();

            return session;
        }

        public async Task AddToDbAsync(LudoContext context)
        {
            context = new LudoContext();

            //If exists do update instead
            if (context.Session.Any(s => s.SessionId == this.SessionId))
            {
                context.Session.Update(this);
            }
            else
            {
                context.Session.Add(this);
            }

            await context.SaveChangesAsync();

        }

        public void RemoveFromDb(LudoContext context)
        {
            context = new LudoContext();

            if (context.Session.Any(s => s.SessionId == this.SessionId))
            {
                context.Session.Remove(this);
            }

            context.SaveChanges();
        }
    }
}
