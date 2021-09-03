using System.ServiceModel;
using System.Threading.Tasks;

namespace Gamer.Manager.Turn.Interface
{

	[ServiceContract]
	public interface ITurnManager
	{

		[OperationContract]
		Task<TurnResponse> PlayTurn(TurnRequest turnRequest);

	}

}