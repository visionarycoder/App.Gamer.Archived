using Client.Content.ConsoleApp.Infrastructure.Data;

namespace Client.Content.ConsoleApp.Resources.Data.GamerDB.Models
{
    public class Player : Entity
    {
        public string Name { get; set; }

        // Navigation property for Tokens
        public List<Token> Tokens { get; set; }

        // Navigation property for Sessions
        public List<Session> Sessions { get; set; }
    }
}
