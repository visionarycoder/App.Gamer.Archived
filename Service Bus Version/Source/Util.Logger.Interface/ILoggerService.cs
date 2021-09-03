using System.ServiceModel;

namespace Gamer.Util.Logger.Interface
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ILoggerService" in both code and config file together.
	[ServiceContract]
	public interface ILoggerService
	{
		[OperationContract]
		void DoWork();
	}
}
