using System.ServiceModel;

namespace Gamer.Util.PubSub.Interface
{

	[ServiceContract]
	public interface IGamerPublishEvent
	{

		[OperationContract]
		void RaiseStartGameEvent(MessageRequest startGameRequest);

		[OperationContract]
		void RaiseEndGameEvent(MessageRequest endGameRequest);

		[OperationContract]
		void RaisePlayTurnEvent(MessageRequest playTurnRequest);

	}

}