using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Gamer.Engine.Game.Interface
{

	[ServiceContract]
	public interface IGameEngine
	{

		[OperationContract]
		Task<bool> CreateGame(Guid gameId, Guid[] playerIds);

		[OperationContract]
		Task<bool> DestroyGame(Guid gameId);

		[OperationContract]
		Task<bool> IsCurrentPlayer(Guid playerId);

		[OperationContract]
		Task<Guid?> GetNextPlayer(Guid gameId);

	}

}