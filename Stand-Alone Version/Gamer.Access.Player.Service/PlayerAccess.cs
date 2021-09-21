using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Gamer.Access.Player.Interface;

namespace Gamer.Access.Player.Service
{

	public class PlayerAccess : IPlayerAccess
	{

		private static readonly HashSet<Interface.Player> cache;

		static PlayerAccess()
		{
			cache = new HashSet<Interface.Player>();
		}

		public async Task<Interface.Player> GetPlayer(Guid playerId)
		{

			var player = cache.FirstOrDefault(i => i.Id == playerId);
			return await Task.FromResult(player);

		}

		public async Task<Interface.Player[]> FindPlayers(Func<Interface.Player, bool> filter)
		{

			var players = cache.Where(filter).ToArray();
			return await Task.FromResult(players);

		}

		public async Task<bool> DeletePlayer(Guid playerId)
		{

			var count = cache.RemoveWhere(i => i.Id == playerId);
			return await Task.FromResult(count > 0);

		}

		public async Task<Interface.Player> CreatePlayer(Interface.Player player)
		{

			if (cache.FirstOrDefault(i => i.Id == player.Id) == null)
			{
				cache.Add(player);
				Trace.WriteLine($"Adding player: {player.Id} / {player.Name} / {player.IsMachine} / {player.GamePiece}");
			}
			return await Task.FromResult(player);

		}

		public async Task<Interface.Player[]> CreatePlayers(Interface.Player[] players)
		{
			
			var list = new List<Interface.Player>();
			foreach (var player in players)
			{
				list.Add(await CreatePlayer(player));
			}
			return list.ToArray();

		}

	}

}
