using System;
using System.Threading.Tasks;

using Gamer.Framework;

namespace Gamer.Access.GameSession.Interface
{

	public interface IGameSessionAccess : IServiceBase
	{

		Task<CreateGameSessionResponse> CreateGameSessionAsync(CreateGameSessionRequest request);
		Task<UpdateGameSessionResponse> UpdateGameSessionAsync(UpdateGameSessionRequest request);
		Task<GetGameSessionResponse> GetGameSessionAsync(GetGameSessionRequest request);

	}
}