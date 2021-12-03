using System;
using System.Threading.Tasks;

namespace Gamer.Component.Access.GameDefinition
{

    public interface IGameDefinitionAccess
    {

        Task<GameDefinition> RetrieveGameDefinition(Guid gameDefinitionId);
        Task<GameDefinition[]> FindGameDefinitions(Func<GameDefinition, bool> filter);

    }

}