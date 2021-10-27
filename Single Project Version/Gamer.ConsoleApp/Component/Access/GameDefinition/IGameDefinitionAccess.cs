using System;
using System.Threading.Tasks;

namespace Gamer.Component.Access.GameDefinition
{

	public interface IGameDefinitionAccess
	{

		Task<GameDefinition[]> FindGameDefinitions(Func<GameDefinition, bool> filter);

	}

}