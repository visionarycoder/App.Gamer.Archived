using System;
using System.Threading.Tasks;

namespace Gamer.Component.Access.GameSession
{
    public interface IGameSessionAccess
    {

        Task<GameSession> ProvisionGameSession(GameSession gameSession);
        Task<GameSession> UpdateGameSession(GameSession gameSession);
        Task<GameSession[]> FindGameSessions(Func<GameSession, bool> filter);
        Task<bool> RemoveGameSession(GameSession gameSession);

    }


}