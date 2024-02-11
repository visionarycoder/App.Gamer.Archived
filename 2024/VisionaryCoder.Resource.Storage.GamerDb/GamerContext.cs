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

    private readonly GamerContext context;

    public GamerDbInitializer(GamerContext context)
    {
        this.context = context;
    }

    public void Initialize()
    {
        context.Database.EnsureCreated();
    }

    public void Seed()
    {

    }

}
