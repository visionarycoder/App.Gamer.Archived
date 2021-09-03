using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Gamer.Manager.Player.Interface
{

	[ServiceContract]
	public interface IPlayerManager
	{

		[OperationContract]
		Task<bool> StartNewGame(int playerCount);

		[OperationContract]
		Task<bool> EndGame(Guid gameId);

	}

}