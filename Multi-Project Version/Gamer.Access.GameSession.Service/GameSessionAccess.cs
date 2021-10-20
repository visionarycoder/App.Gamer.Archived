using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Gamer.Access.GameSession.Interface;

namespace Gamer.Access.GameSession.Service
{

	public class GameSessionAccess : IGameSessionAccess
	{

		private static readonly HashSet<Interface.GameSession> cache;

		static GameSessionAccess()
		{
			cache = new HashSet<Interface.GameSession>();
		}

		public async Task<Interface.GameSession> CreateGameSession(Interface.GameSession gameSession)
		{
			if(cache.FirstOrDefault(i => i.Id == gameSession.Id) == null)
				cache.Add(gameSession);
			return await Task.FromResult(gameSession);
		}

		public async Task<Interface.GameSession> UpdateGameSession(Interface.GameSession gameSession)
		{

			var cached = cache.FirstOrDefault(i => i.Id == gameSession.Id);
			if (cached != null)
			{
				cached.CurrentPlayerId = gameSession.CurrentPlayerId;
				Trace.WriteLine($"Updating game session {gameSession.Id} -> {gameSession.CurrentPlayerId} ");
			}
			return await Task.FromResult(cached);

		}

		public async Task<Interface.GameSession[]> FindGameSession(Func<Interface.GameSession, bool> filter)
		{

			var cached = cache.Where(filter).ToArray();
			return await Task.FromResult(cached);

		}

		public async Task<Interface.GameSession> GetGameSession(Guid gameSessionId)
		{

			var gameSession = cache.FirstOrDefault(i => i.Id == gameSessionId);
			return await Task.FromResult(gameSession);

		}

	}

}