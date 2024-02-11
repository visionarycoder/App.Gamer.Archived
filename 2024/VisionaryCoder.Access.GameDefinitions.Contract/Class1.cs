using VisionaryCoder.ifx;

namespace VisionaryCoder.Access.GameDefinitions.Contract;

public class GameDefinition
{
    public Identifier Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<GameDefinitionAttribute> Attributes { get; set; }
    public ICollection<IGameToken> GameTokens { get; set; }
}

public interface IGameDefinitionAccess
{

}