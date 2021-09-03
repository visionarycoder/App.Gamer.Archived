using System.ServiceModel;

namespace Gamer.Util.PubSub
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPubSubService" in both code and config file together.
	[ServiceContract]
	public interface IPubSubService
	{
		[OperationContract]
		void DoWork();
	}
}
