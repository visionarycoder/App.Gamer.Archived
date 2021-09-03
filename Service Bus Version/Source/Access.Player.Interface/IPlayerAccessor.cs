using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Gamer.Access.Player.Interface
{

	[ServiceContract]
	public interface IPlayerAccessor
	{

		[OperationContract]
		Task <Player> GetPlayer(Guid playerId);

		[OperationContract]
		Task<Player[]> GetPlayers(Guid gameId);

		[OperationContract]
		Task<bool> CreatePlayers(Player[] players);

		[OperationContract]
		Task<bool> DeletePlayers(Player[] players);


	}

}