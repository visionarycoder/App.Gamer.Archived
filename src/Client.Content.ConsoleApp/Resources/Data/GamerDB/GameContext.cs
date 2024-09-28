using Client.Content.ConsoleApp.Resources.Data.GamerDB.Models;
using Microsoft.EntityFrameworkCore;

namespace Client.Content.ConsoleApp.Resources.Data.GamerDB
{
    public class GameContext(DbContextOptions<GameContext> options) : DbContext(options)
    {
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Definition> Definitions { get; set; }
        public DbSet<Workflow> Workflows { get; set; }
        public DbSet<Rule> Rules { get; set; }
        public DbSet<Token> Tokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Definition entity
            modelBuilder.Entity<Definition>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.HasMany(e => e.Workflows).WithOne(w => w.Definition).HasForeignKey(w => w.DefinitionId).OnDelete(DeleteBehavior.Restrict);
                entity.HasMany(e => e.Players).WithOne().OnDelete(DeleteBehavior.Restrict);
                entity.HasMany(e => e.Sessions).WithOne(s => s.Definition).HasForeignKey(s => s.DefinitionId);
            });

            // Configure Session entity
            modelBuilder.Entity<Session>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(s => s.Definition).WithMany(d => d.Sessions).HasForeignKey(s => s.DefinitionId);
                entity.HasMany(s => s.Players).WithMany(p => p.Sessions);
            });

            // Configure Player entity
            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasMany(p => p.Tokens).WithOne(t => t.Player).HasForeignKey(t => t.PlayerId);
                entity.HasMany(p => p.Sessions).WithMany(s => s.Players);
            });

            // Configure Token entity
            modelBuilder.Entity<Token>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TokenValue).IsRequired();
                entity.HasOne(t => t.Player).WithMany(p => p.Tokens).HasForeignKey(t => t.PlayerId);
            });

            // Configure Workflow entity
            modelBuilder.Entity<Workflow>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.WorkflowName).IsRequired();
                entity.HasMany(e => e.Rules).WithOne().OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(w => w.Definition).WithMany(d => d.Workflows).HasForeignKey(w => w.DefinitionId);
            });

            // Configure Rule entity
            modelBuilder.Entity<Rule>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.RuleName).IsRequired();
                entity.Property(e => e.Expression).IsRequired();
                entity.HasOne(r => r.Definition).WithMany(d => d.Rules).HasForeignKey(r => r.DefinitionId);
            });

            // Seed data
            var tokens = new List<Token>
                    {
                        new Token { Id = 1, TokenValue = "X", PlayerId = 1 },
                        new Token { Id = 2, TokenValue = "O", PlayerId = 2 }
                    };

            var players = new List<Player>
                    {
                        new Player { Id = 1, Name = "Player1" },
                        new Player { Id = 2, Name = "Player2" }
                    };

            var rules = new List<Rule>
                    {
                        new Rule { Id = 1, RuleName = "PlayerCountRule", Expression = "input1 >= 2 && input1 <= 2", DefinitionId = 1 }
                    };

            var workflows = new List<Workflow>
                    {
                        new Workflow { Id = 1, WorkflowName = "PlayerCountWorkflow", Rules = rules, DefinitionId = 1 }
                    };

            var definition = new Definition
            {
                Id = 1,
                Name = "Tic-Tac-Toe",
                GameName = "Tic-Tac-Toe",
                Workflows = workflows,
                Players = players,
                MinPlayers = 2,
                MaxPlayers = 2
            };

            modelBuilder.Entity<Token>().HasData(tokens);
            modelBuilder.Entity<Player>().HasData(players);
            modelBuilder.Entity<Rule>().HasData(rules);
            modelBuilder.Entity<Workflow>().HasData(workflows);
            modelBuilder.Entity<Definition>().HasData(definition);
        }
    }

}