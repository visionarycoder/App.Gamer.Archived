using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Gamer.Client.ConsoleApp.Data
{
    internal class GameDefinition
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Guid> GamePieceIds { get; set; }
        public int MaxNumberOfPlayers { get; set; }
        public int MinNumberOfPlayers { get; set; }
        public string TurnPrompt { get; set; }
        public virtual ICollection<GamePiece> GamePieces { get; set; }
    }

    public class GamePiece
    {

        public Guid Id { get; set; }
        public Guid GameSessionId { get; set; }
        public Guid PlayerId { get; set; }

        public string Label { get; set; }

        public byte[] Image { get; set; }
        public string ImagePath { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

    }

    public class GameSession
    {

        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public List<Guid> PlayerIds { get; set; } = new List<Guid>();
        public virtual ICollection<Player> Players { get; set; } = new List<Player>();

    }

    public class Player
    {

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = "Undefined";
        public bool IsMachine { get; set; }
        public List<Guid> GamePieceIds { get; set; } = new List<Guid>();

        public virtual ICollection<GamePiece> GamePieces { get; set; } = new List<GamePiece>();

    }

    public class GamerDbContext : DbContext
    {

        public string ConnectionString { get; }

        public GamerDbContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public DbSet<GameDefinition> GameDefinitions { get; set; }
        public DbSet<GameSession> GameSessions { get; set; }
        public DbSet<GamePiece> GamePieces { get; set; }
        public DbSet<Player> Players { get; set; }

    }

    public static class GamerDbInitializer
    {

        /// <summary>
        /// Load sample data.
        /// </summary>
        /// 
        /// <param name="db"></param>
        public static async Task Initialize(GamerDbContext db)
        {

            await InitializeGameDefinition(db);
            await InitializeCards(db);

        }

        private static async Task InitializeGameDefinition(GamerDbContext db)
        {
            if (db.GameDefinitions.Any())
                return;

            var gameDefinition = new GameDefinition
            {
                Id = Guid.NewGuid(),
                Name = "Tic-Tac-Toe",
                Description = "The Classic three across game.  Also known as 'noughts and crosses' or 'Xs and Os.'",
                GamePieces = new[] { new GamePiece { Id = Guid.NewGuid(), Label = "X" }, new GamePiece { Id = Guid.NewGuid(), Label = "O" } },
                MaxNumberOfPlayers = 2,
                MinNumberOfPlayers = 0,
                TurnPrompt = "Your turn.",
            };
            await db.GameDefinitions.AddAsync(gameDefinition);
            await db.SaveChangesAsync();
        }

        private static async Task InitializeCards(GamerDbContext db)
        {

            foreach (var suit in new[] { "Club", "Spade", "Heart", "Diamond" })
            {
                foreach (var values in new[] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" })
                {
                    var card = new Card
                    {
                        Name = values,
                        Value = $"{suit}|{values}"
                    };
                    await db.Cards.AddAsync(card);
                }
            }
            await db.SaveChangesAsync();

        }

    }
}
