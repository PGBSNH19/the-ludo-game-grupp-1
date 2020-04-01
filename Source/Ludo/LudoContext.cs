using EngineClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ludo
{
    public class LudoContext : DbContext
    {
        public DbSet<Session> Session { get; set; }
        public DbSet<Player> Player { get; set; }
        public DbSet<GamePiece> GamePiece { get; set; }
        public DbSet<GameLog> GameLog { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionSetup.GetConnectionString());
        }
    }
}
