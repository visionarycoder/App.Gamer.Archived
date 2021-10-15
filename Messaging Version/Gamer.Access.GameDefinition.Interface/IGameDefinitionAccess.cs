using System;
using System.Threading.Tasks;
using Gamer.Framework;

namespace Gamer.Access.GameDefinition.Interface
{

	public interface IGameDefinitionAccess : IServiceBase
	{

		Task<GetGameDefinitionsResponse> GetGameDefinitionsAsync(GetGameDefinitionsRequest request);
		Task<GetGameDefinitionResponse> GetGameDefinitionAsync(GetGameDefinitionRequest request);

	}
}