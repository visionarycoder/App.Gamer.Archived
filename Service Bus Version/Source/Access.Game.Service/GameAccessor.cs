using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Gamer.Access.Game.Interface;
using Gamer.Framework.Helpers;

namespace Gamer.Access.Game.Service
{


	public class GameAccessor : IGameAccessor
	{

		private readonly GameDB defaultContext;

		public GameAccessor()
		{
			defaultContext = null;
		}

		public GameAccessor(GameDB gameDb)
		{
			defaultContext = gameDb;
		}

		public async Task<bool> CreateGame(Guid gameId, Guid[] playerIds)
		{

			var context = DbContextHelper<GameDB>.GetContext(defaultContext);
			var game = new Interface.Game
			{
				GameId = gameId,
				PlayerIds = playerIds,
				CurrentPlayerId = playerIds[0]
			};
			context.Games.Add(game);
			var count = await context.SaveChangesAsync();
			return count == 1;

		}

		public async Task<bool> IncrementPlayer(Guid gameId)
		{

			var context = DbContextHelper<GameDB>.GetContext(defaultContext);
			var game = await context.Games.SingleOrDefaultAsync(i => i.GameId == gameId);
			if (game.CurrentPlayerId == null)
			{
				game.CurrentPlayerId = game.PlayerIds[0];
				return true;
			}

			var currentPlayerId = (Guid)game.CurrentPlayerId;
			if (!game.PlayerIds.Contains(currentPlayerId))
				throw new ApplicationException("Unable to find the player id in player id collection");

			var idx = game.PlayerIds.ToList().IndexOf(currentPlayerId) + 1;
			var length = game.PlayerIds.Length;
			if (idx >= length)
				idx = 0;
			game.CurrentPlayerId = game.PlayerIds[idx];
			return true;

		}

		public async Task<Guid?> GetCurrentPlayer(Guid gameId)
		{

			var context = DbContextHelper<GameDB>.GetContext(defaultContext);
			var game = await context.Games.FirstOrDefaultAsync(i => i.GameId == gameId);
			return game.CurrentPlayerId;

		}

		public async Task<bool> DeleteGame(Guid gameId)
		{

			var context = DbContextHelper<GameDB>.GetContext(defaultContext);
			var game = await context.Games.FirstOrDefaultAsync(i => i.GameId == gameId);
			context.Games.Remove(game);
			var count = await context.SaveChangesAsync();
			return count == 1;

		}

	}

}
