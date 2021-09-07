using System.Threading.Tasks;

namespace TicTacToe.Access.GameDefinition
{
    public interface IGameDefinitionAccess
    {
        
        Task<GameDefinition[]> GetGameDefinitions();

    }
}