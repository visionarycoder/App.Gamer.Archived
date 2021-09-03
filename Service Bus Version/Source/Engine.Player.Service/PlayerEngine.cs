using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gamer.Access.Player.Interface;
using Gamer.Access.Player.Service;
using Gamer.Engine.Player.Interface;
using ServiceModelEx;

namespace Gamer.Engine.Player.Service
{

	public class PlayerEngine : IPlayerEngine
	{

		public async Task<bool> InitializePlayers(Guid gameId, int playerCount)
		{

			var accessor = InProcFactory.CreateInstance<PlayerAccessor, PlayerAccessor>();

			var players = new List<Access.Player.Interface.Player>();
			var player = new Access.Player.Interface.Player
			{
				GameId = gameId,
				GamePiece = "X",
				PlayerName = "Player 1",
				IsMachine = false,
			};
			players.Add(player);

			player = new Access.Player.Interface.Player
			{
				GameId = gameId,
				GamePiece = "O",
				PlayerName = "Player 2",
				IsMachine = playerCount == 1,
			};
			players.Add(player);

			return await accessor.CreatePlayers(players.ToArray());

		}

		public async Task<bool> TearDownPlayers(Guid gameId)
		{

			var accessor = InProcFactory.CreateInstance<PlayerAccessor, PlayerAccessor>();
			var players = await accessor.GetPlayers(gameId);
			return await accessor.DeletePlayers(players);

		}

		public async Task<Access.Player.Interface.Player> GetPlayer(Guid playerId)
		{

			var accessor = InProcFactory.CreateInstance<PlayerAccessor, IPlayerAccessor>();
			return await accessor.GetPlayer(playerId);

		}

		public async Task<Access.Player.Interface.Player[]> GetPlayers(Guid gameId)
		{

			var accessor = InProcFactory.CreateInstance<PlayerAccessor, IPlayerAccessor>();
			return await accessor.GetPlayers(gameId);

		}

	}

}