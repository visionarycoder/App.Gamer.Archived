using Microsoft.EntityFrameworkCore;

namespace Client.Content.ConsoleApp
{
    public class GameContext : DbContext
    {
        public DbSet<GameState> GameStates { get; set; }
        public DbSet<Player> Players { get; set; } // Add DbSet for Player

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=games.db");
        }
    }
}
