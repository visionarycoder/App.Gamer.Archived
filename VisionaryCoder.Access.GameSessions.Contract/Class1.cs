using VisionaryCoder.ifx;

namespace VisionaryCoder.Access.GameSessions.Contract;

public class GameSession
{
    
    public Identifier GameSessionIdentifier { get; set; }
    public ICollection<Identifier> PlayerIdentifiers { get; set; }
    public Identifier GameDefinitionIdentifier { get; set; }
    public Identifier GameStatusIdentifier { get; set; }

}

public class GameStatus
{
    public Identifier GameStatusIdentifier { get; set; }
    public string Value { get; set; }
}