using System.ComponentModel.DataAnnotations;
using Client.Content.ConsoleApp.Infrastructure.Data;

namespace Client.Content.ConsoleApp.Resources.Data.GamerDB.Models
{
    public class Rule : Entity
    {
        [Required]
        public string RuleName { get; set; }

        [Required]
        public string Expression { get; set; }

        // Foreign key for Definition
        public int GameDefinitionId { get; set; }

        // Navigation property for Definition
        public Definition Definition { get; set; }
    }
}
