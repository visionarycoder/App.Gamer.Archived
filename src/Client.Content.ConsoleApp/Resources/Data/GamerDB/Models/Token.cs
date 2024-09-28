using Client.Content.ConsoleApp.Infrastructure.Data;

namespace Client.Content.ConsoleApp.Resources.Data.GamerDB.Models
{
    public class Token : Entity
    {
        public string TokenValue { get; set; }

        // Foreign key for Player
        public int PlayerId { get; set; }

        // Navigation property for Player
        public Player Player { get; set; }
    }
}
