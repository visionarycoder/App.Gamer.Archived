using System.ServiceModel;

namespace Gamer.Framework.Messaging
{

	[ServiceContract]
	public interface ISubscriptionService
	{

		[OperationContract]
		void Subscribe(string eventOperation);

		[OperationContract]
		void Unsubscribe(string eventOperation);

	}

}