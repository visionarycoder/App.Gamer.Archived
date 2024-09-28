using System.ComponentModel.DataAnnotations;

namespace Client.Content.ConsoleApp.Infrastructure.Data
{
    public abstract class Entity
    {
        [Required, Key] public int Id { get; set; }
    }
}
