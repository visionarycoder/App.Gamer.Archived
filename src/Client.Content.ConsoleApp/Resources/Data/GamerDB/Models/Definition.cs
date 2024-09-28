using Client.Content.ConsoleApp.Infrastructure.Data;

namespace Client.Content.ConsoleApp.Resources.Data.GamerDB.Models
{
    public class Definition : Entity
    {

        public string Name { get; set; }

        public List<Workflow> Workflows { get; set; }

        public int MinPlayers { get; set; }

        public int MaxPlayers { get; set; }

        // Collection of Sessions
        public List<Session> Sessions { get; set; }
    }

}
