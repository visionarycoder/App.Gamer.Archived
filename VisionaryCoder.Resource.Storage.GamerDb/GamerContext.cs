using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VisionaryCoder.Resource.Data.GamerDb.Models;

namespace VisionaryCoder.Resource.Data.GamerDb;

    public class GamerContext : DbContext
    {

        public DbSet<GameDefinition> GameDefinitions { get; set; } = null!;
        public DbSet<GameType> GameTypes { get; set; } = null!;
        public DbSet<GameDefinitionAttribute> GameDefinitionAttributes { get; set; } = null!;
        public DbSet<GameSession> GameSessions { get; set; } = null!;
        public DbSet<GamePlayer> GamePlayers { get; set; } = null!;

        public GamerContext(DbContextOptions<GamerContext> options)
            : base(options)
        {


        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        //private void OnModelCreating(EntityTypeBuilder<>)
    }


public class GamerDbInitializer
{

    public static void Initialize(GamerDb.GamerContext context)
    {
        context.Database.EnsureCreated();
    }

    public static void SeedGameTypes(GamerDb.GamerContext context)
    {

        var gameSeeds = new Dictionary<string, List<string>>
        {
            //("Games for two.", new List<string>{ "All Fours", "Backgammon"})
            //"Games for three.",
            //"Games for four and partnership games."
            //"Fun and Family"
        };

        //foreach (var name in names)
        //{
        //    var entity = new GamerDb.Models.GameType
        //    {
        //        Name = name,
        //    };
        //    context.GameTypes.Add(entity);
        //}
        //context.SaveChanges();


    }

}
