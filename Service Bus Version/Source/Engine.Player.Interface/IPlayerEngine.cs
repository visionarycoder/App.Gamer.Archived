using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Gamer.Engine.Player.Interface
{

	[ServiceContract]
	public interface IPlayerEngine
	{

		[OperationContract]
		Task<bool> InitializePlayers(Guid gameId, int playerCount);

		[OperationContract]
		Task<bool> TearDownPlayers(Guid gameId);

		[OperationContract]
		Task<Access.Player.Interface.Player> GetPlayer(Guid playerId);

		Task<Access.Player.Interface.Player[]> GetPlayers(Guid gameId);

	}

}