using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Gamer.Manager.Game.Interface
{

	[ServiceContract]
	public interface IGameManger
	{

		[OperationContract]
		Task<GameCreateResponse> StartGame(GameCreateRequest gameDefintion);

		[OperationContract]
		Task<bool> EndGame(Guid gameId);

	}

}
