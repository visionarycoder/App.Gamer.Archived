using System.Threading.Tasks;

namespace Gamer.StandAlone.Components.Access.GameDefinition
{
    public interface IGameDefinitionAccess
    {
        
        Task<GameDefinition[]> GetGameDefinitions();

    }
}