using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Access.GameSession.Interface;
using Util.Caching;
using Util.ServiceMessaging;

namespace Access.GameSession.Service
{
    
    public class GameSessionAccess : IGameSessionAccess
    {

        private static readonly HashSet<GameState> gameStateCache = new HashSet<GameState>();
        private static readonly HashSet<Interface.GameSession> gameSessionCache = new HashSet<Interface.GameSession>();

        public async Task<CreateGameSessionResponse> CreateGameSession(CreateGameSessionRequest request)
        {
            var response = ServiceMessageFactory<CreateGameSessionResponse>.CreateFrom(request);
            var gameSession = GameSessionFactory.Create();
            gameSessionCache.Add(gameSession);
            response.GameSession = gameSession;
            return await Task.FromResult(response);
        }

        public async Task<RetrieveGameSessionResponse> RetrieveGameSession(RetrieveGameSessionRequest request)
        {
            var response = ServiceMessageFactory<RetrieveGameSessionResponse>.CreateFrom(request);
            var gameSession = gameSessionCache.FirstOrDefault(i => i.Id == request.GameSessionId);
            if (gameSession != null)
            {
                response.GameSession = gameSession;
            }
            else
            {
                response.Errors = "No existing session found.";
            }
            return await Task.FromResult(response);
        }

        public async Task<ApplyGameSessionChangeResponse> ApplyGameSessionChange(ApplyGameSessionChangesRequest request)
        {
            var response = ServiceMessageFactory<ApplyGameSessionChangeResponse>.CreateFrom(request);
            var gameSession = gameSessionCache.FirstOrDefault(i => i.Id == request.GameSession.Id);
            if (gameSession != null)
            {
                gameSession.GameState = request.GameSession.GameState;
                gameSession.Players = request.GameSession.Players;
                response.GameSession = gameSession;
            }
            else
            {
                response.Errors = "No existing session found.";
            }
            return await Task.FromResult(response);
        }

    }

}
