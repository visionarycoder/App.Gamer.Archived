using System;
using System.Threading.Tasks;
using Gamer.Access.Game.Interface;
using Gamer.Access.Game.Service;
using Gamer.Engine.Game.Interface;
using ServiceModelEx;

namespace Gamer.Engine.Game.Service
{

	public class GameEngine : IGameEngine
	{

		public async Task<bool> CreateGame(Guid gameId, Guid[] playerIds)
		{

			var accessor = InProcFactory.CreateInstance<GameAccessor, IGameAccessor>();
			return await accessor.CreateGame(gameId, playerIds);

		}

		public async Task<bool> DestroyGame(Guid gameId)
		{

			var accessor = InProcFactory.CreateInstance<GameAccessor, IGameAccessor>();
			return await accessor.DeleteGame(gameId);

		}

		public async Task<bool> IsCurrentPlayer(Guid playerId)
		{

			var accessor = InProcFactory.CreateInstance<GameAccessor, IGameAccessor>();
			var recordedPlayerId = await accessor.GetCurrentPlayer(playerId);
			return recordedPlayerId == playerId;

		}

		public async Task<Guid?> GetNextPlayer(Guid gameId)
		{

			var accessor = InProcFactory.CreateInstance<GameAccessor, IGameAccessor>();
			var wasSuccessful = await accessor.IncrementPlayer(gameId);
			if (wasSuccessful)
				return await accessor.GetCurrentPlayer(gameId);
			throw new ApplicationException("Unable to increment the player");

		}

	}

}