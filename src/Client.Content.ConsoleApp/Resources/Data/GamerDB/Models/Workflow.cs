using System.ComponentModel.DataAnnotations;
using Client.Content.ConsoleApp.Infrastructure.Data;

namespace Client.Content.ConsoleApp.Resources.Data.GamerDB.Models
{
    public class Workflow : Entity
    {
        [Required]
        public string WorkflowName { get; set; }

        public List<Rule> Rules { get; set; }

        // Foreign key for Definition
        public int GameDefinitionId { get; set; }

        // Navigation property for Definition
        public Definition Definition { get; set; }
    }
}
