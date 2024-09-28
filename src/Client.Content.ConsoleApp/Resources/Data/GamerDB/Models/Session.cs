using Client.Content.ConsoleApp.Infrastructure.Data;

namespace Client.Content.ConsoleApp.Resources.Data.GamerDB.Models
{

    public class Session : Entity
    {
        public int CurrentPlayerIndex { get; set; }
        public Board Board { get; set; }

        // Navigation property for Players
        public List<Player> Players { get; set; }

        // Foreign key for Definition
        public int DefinitionId { get; set; }

        // Navigation property for Definition
        public Definition Definition { get; set; }
    }

}