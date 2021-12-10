using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gamer.Access.Player.Interface;
using Gamer.Framework;
using Gamer.Utility.ServiceMessaging;
using Microsoft.Extensions.Logging;

namespace Gamer.Access.Player.Service
{

	public class PlayerAccess : ServiceBase, IPlayerAccess
	{

		private static readonly HashSet<Interface.Player> cache;

		static PlayerAccess()
		{
			cache = new HashSet<Interface.Player>();
		}

		public PlayerAccess(ILogger logger)
			: base(logger)
		{
		}

		public async Task<GetPlayerResponse> GetPlayerAsync(GetPlayerRequest request)
		{
			var response = ServiceMessageFactory<GetPlayerResponse>.CreateFrom(request);
			response.Player = cache.FirstOrDefault(i => i.Id == request.PlayerId);
			if (response.Player == null)
			{
				response.Errors = "Unable to find player.";
			}
			return await Task.FromResult(response);
		}

		public async Task<CreatePlayersResponse> CreatePlayersAsync(CreatePlayersRequest request)
		{

			var response = ServiceMessageFactory<CreatePlayersResponse>.CreateFrom(request);
			var list = new List<Interface.Player>();
			foreach (var player in request.Players)
			{
				cache.Add(player);
				list.Add(player);
			}
			response.Players = list.ToArray();

			if (! cache.IsProperSupersetOf(request.Players))
			{
				response.Errors = "Unable to create all of the players.";
			}
			return response;

		}

	}

}
