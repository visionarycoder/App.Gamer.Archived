using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gamer.StandAlone.Components.Access.GameSessions
{

    public class GameSessionAccess : IGameSessionAccess
    {

        private static readonly HashSet<GameSession> cache;

        static GameSessionAccess()
        {
            cache = new HashSet<GameSession>();
        }
        
        public async Task<GameSession> CreateGameSession()
        {
            var gameSession = new GameSession();
            return await Task.FromResult(gameSession);
        }

        public async Task<GameSession> UpdateGameSession(GameSession gameSession)
        {
            
            var cached = cache.FirstOrDefault(i => i.Id == gameSession.Id);
            if (cached == null)
            {
                var newGameSession = await CreateGameSession();
                cache.Add(newGameSession);
                return newGameSession;
            }

            cached.GameStatus = gameSession.GameStatus;
            cached.PlayerIds = gameSession.PlayerIds;
            return cached;

        }

        public async Task<GameSession> FindGameSession(Guid gameSessionId)
        {

            var cached = cache.FirstOrDefault(i => i.Id == gameSessionId);
            return await Task.FromResult(cached);

        }

    }
}