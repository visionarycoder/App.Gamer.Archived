using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Gamer.Component.Access.GameSession
{

    public class GameSessionAccess : IGameSessionAccess
    {

        private static readonly HashSet<GameSession> cache;

        static GameSessionAccess()
        {
            cache = new HashSet<GameSession>();
        }

        public async Task<GameSession> ProvisionGameSession(GameSession gameSession)
        {
            if (cache.FirstOrDefault(i => i.Id == gameSession.Id) == null)
                cache.Add(gameSession);
            return await Task.FromResult(gameSession);
        }

        public async Task<GameSession> UpdateGameSession(GameSession gameSession)
        {

            var cached = cache.FirstOrDefault(i => i.Id == gameSession.Id);
            if (cached != null)
            {
                cached.CurrentPlayerId = gameSession.CurrentPlayerId;
                Trace.WriteLine($"Updating game session {gameSession.Id} -> {gameSession.CurrentPlayerId} ");
            }
            return await Task.FromResult(cached);

        }

        public async Task<GameSession[]> FindGameSessions(Func<GameSession, bool> filter)
        {

            var results = filter != null ? cache.Where(filter) : cache.AsQueryable();
            return await Task.FromResult(results.ToArray());

        }

        public async Task<bool> RemoveGameSession(GameSession gameSession)
        {
            if (!cache.Contains(gameSession))
                return false;

            cache.Remove(gameSession);
            return await Task.FromResult(true);
        }
    }

}