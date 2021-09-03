using System;
using System.ServiceModel;
using Gamer.Util.PubSub.Interface;
using ServiceModelEx;

namespace Gamer.Util.PubSub.Service
{

	[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
	public class GamerPublishService : PublishService<IGamerPublishEvent>, IGamerPublishEvent
	{

		private GamerSubscriptionService subscribers;

		public void RaiseStartGameEvent(MessageRequest startGameRequest)
		{
			
		}

		public void RaiseEndGameEvent(MessageRequest endGameRequest)
		{
			throw new NotImplementedException();
		}

		public void RaisePlayTurnEvent(MessageRequest playTurnRequest)
		{
			throw new NotImplementedException();
		}

	}

}