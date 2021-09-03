using System.ServiceModel;

namespace Gamer.Infrastructure.Contract
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