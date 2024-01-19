using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisionaryCoder.Resource.Data.GamerDb.Models;

public class Identifier
{

}

public class EntityBase
{
    public int Id { get; set; }
    public Guid Uuid { get; set; } = Guid.NewGuid();
    public bool IsEnabled { get; set; }
    public string CreatedBy { get; set; } = "Unknown";
    public DateTime CreatedOnUtc { get; set; } = DateTime.UtcNow;
    public string UpdatedBy { get; set; } = "Unknown";
    public DateTime UpdatedOnUtc { get; set; } = DateTime.UtcNow;
}

public class GameDefinition : EntityBase
{

    public int GameTypeId { get; set; }
    public virtual GameType GameType { get; set; }
    public ICollection<GameDefinitionAttribute> GameDefinitionAttributes { get; set; }
}

public class GameType : EntityBase
{
    public string Name { get; set; }
    public string Description { get; set; }
}

public class GameDefinitionAttribute : EntityBase
{
    public string Name { get; set; }
    public string Value { get; set; }
    public string ValueType { get; set; }
}

public class GameSession : EntityBase { }
public class GamePlayer : EntityBase { }