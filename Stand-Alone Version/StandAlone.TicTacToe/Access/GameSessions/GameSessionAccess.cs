using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Access.GameSessions
{


    public interface IGameSessionAccess
    {

        Task<GameSession> CreateGameSession();
        Task<GameSession> UpdateGameSession(GameSession gameSession);

    }

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

    }

    public class GameSession
    {

        public Guid Id { get; set; }
        public GameStatus GameStatus { get; set; }
        public Guid PlayerIds { get; set; }

    }

}