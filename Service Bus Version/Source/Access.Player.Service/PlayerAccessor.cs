using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Gamer.Access.Player.Interface;
using Gamer.Framework.Helpers;

namespace Gamer.Access.Player.Service
{

	public class PlayerAccessor : IPlayerAccessor
	{

		private PlayerDB playerDb = null;

		public PlayerAccessor()
		{
		}

		public PlayerAccessor(PlayerDB context)
		{
			playerDb = context;
		}

		/// <summary>
		/// Get a single player by id 
		/// </summary>
		/// <param name="playerId"></param>
		/// <returns></returns>
		public async Task<Interface.Player> GetPlayer(Guid playerId)
		{

			var context = DbContextHelper<PlayerDB>.GetContext(playerDb);
			return await context.Players.FirstOrDefaultAsync(i => i.PlayerId == playerId);

		}

		/// <summary>
		/// Get a list of players by game id
		/// if game id == Guid.Empty
		///		return all players
		/// </summary>
		/// <param name="gameId"></param>
		/// <returns></returns>
		public async Task<Interface.Player[]> GetPlayers(Guid gameId)
		{

			var context = DbContextHelper<PlayerDB>.GetContext(playerDb);
			if (gameId == Guid.Empty)
				return await context.Players.ToArrayAsync();
			return await context.Players.Where(i => i.GameId == gameId).ToArrayAsync();

		}

		public async Task<bool> CreatePlayers(Interface.Player[] players)
		{

			var context = DbContextHelper<PlayerDB>.GetContext(playerDb);
			context.Players.AddRange(players);
			var count = await context.SaveChangesAsync();
			return count == players.Length;

		}

		public async Task<bool> DeletePlayers(Interface.Player[] players)
		{

			var context = DbContextHelper<PlayerDB>.GetContext(playerDb);
			context.Players.RemoveRange(players);
			var count = await context.SaveChangesAsync();
			return count == players.Length;

		}

	}

}