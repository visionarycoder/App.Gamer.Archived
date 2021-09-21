using System;
using System.Threading.Tasks;

namespace Gamer.Access.GameDefinition.Interface
{

	public interface IGameDefinitionAccess
	{

		Task<GameDefinition[]> GetGameDefinitions();
		Task<GameDefinition> GetGameDefinition(Guid gameDefinitionId);
		Task<GameDefinition[]> FindGameDefinitions(Func<GameDefinition, bool> filter);

	}

}