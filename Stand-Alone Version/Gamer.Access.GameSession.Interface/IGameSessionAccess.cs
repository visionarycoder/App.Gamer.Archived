using System;
using System.Threading.Tasks;

namespace Gamer.Access.GameSession.Interface
{
	public interface IGameSessionAccess
	{

		Task<GameSession> CreateGameSession(GameSession gameSession);
		Task<GameSession> UpdateGameSession(GameSession gameSession);
		Task<GameSession[]> FindGameSession(Func<GameSession, bool> filter);
		Task<GameSession> GetGameSession(Guid gameSessionId);

	}
}