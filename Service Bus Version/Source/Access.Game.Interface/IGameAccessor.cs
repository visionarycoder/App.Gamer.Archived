using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Gamer.Access.Game.Interface
{

	[ServiceContract]
	public interface IGameAccessor
	{

		[OperationContract]
		Task<bool> CreateGame(Guid gameId, Guid[] playerIds);

		[OperationContract]
		Task<bool> IncrementPlayer(Guid gameId);

		[OperationContract]
		Task<Guid?> GetCurrentPlayer(Guid gameId);

		[OperationContract]
		Task<bool> DeleteGame(Guid gameId);

	}

}