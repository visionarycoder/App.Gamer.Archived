using System;
using System.Threading.Tasks;

namespace Gamer.StandAlone.Components.Access.GameSessions
{
    public interface IGameSessionAccess
    {

        Task<GameSession> CreateGameSession();
        Task<GameSession> UpdateGameSession(GameSession gameSession);
        Task<GameSession> FindGameSession(Guid gameSessionId);

    }
}