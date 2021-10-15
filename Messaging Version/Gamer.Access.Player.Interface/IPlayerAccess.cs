using System;
using System.Threading.Tasks;
using Gamer.Framework;

namespace Gamer.Access.Player.Interface
{
	public interface IPlayerAccess : IServiceBase
	{
		
		Task<GetPlayerResponse> GetPlayerAsync(GetPlayerRequest request);
		Task<CreatePlayersResponse> CreatePlayersAsync(CreatePlayersRequest request);

	}
}