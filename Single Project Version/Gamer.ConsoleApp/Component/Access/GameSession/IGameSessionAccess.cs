using System;
using System.Threading.Tasks;

namespace Gamer.Component.Access.GameSession
{
	public interface IGameSessionAccess
	{

		Task<GameSession> CreateGameSession(GameSession gameSession);
		Task<GameSession> UpdateGameSession(GameSession gameSession);
		Task<GameSession[]> FindGameSession(Func<GameSession, bool> filter);
		Task<GameSession> GetGameSession(Guid gameSessionId);

	}
}