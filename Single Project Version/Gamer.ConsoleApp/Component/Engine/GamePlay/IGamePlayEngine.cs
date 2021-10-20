using System;
using System.Threading.Tasks;
using Gamer.Component.Access.GameSession;
using Gamer.Component.Access.Player;

namespace Gamer.Component.Engine.GamePlay
{

	public interface IGamePlayEngine
	{

		Task<bool> IsPlayable(Guid gameSessionId);
		Task<Player> FindWinner(Guid gameSessionId);
		Task<bool> IsTileOpen(Guid gameSessionId, string address);
		Task<GameSession> InitializeGame(Guid gameDefinitionId, int numberOfPlayers);
		Task IncrementPlayer(Guid gameSessionId);
		Task AutoPlayTurn(Guid gameSessionId);
		Task PlayTurn(Guid gameSessionId, Guid playerId, string address);

	}

}