using System;
using System.Threading.Tasks;
using Gamer.Engine.Player.Interface;
using Gamer.Engine.Player.Service;
using Gamer.Manager.Player.Interface;
using ServiceModelEx;

namespace Gamer.Manager.Player.Service
{

	public class PlayerManager : IPlayerManager
	{

		public async Task<bool> StartNewGame(int playerCount)
		{

			var gameId = Guid.NewGuid();
			var engine = InProcFactory.CreateInstance<PlayerEngine, IPlayerEngine>();
			var result = await engine.InitializePlayers(gameId, playerCount);
			return result;

		}

		public async Task<bool> EndGame(Guid gameId)
		{

			var engine = InProcFactory.CreateInstance<PlayerEngine, IPlayerEngine>();
			return await engine.TearDownPlayers(gameId);

		}

	}

}
