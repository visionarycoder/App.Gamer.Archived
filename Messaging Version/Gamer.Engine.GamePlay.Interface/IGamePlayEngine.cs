using System.Threading.Tasks;
using Gamer.Framework;

namespace Gamer.Engine.GamePlay.Interface
{

	public interface IGamePlayEngine : IServiceBase
	{

		Task<IsGamePlayableResponse> IsGamePlayableAsync(IsGamePlayableRequest request);
		Task<FindWinnerResponse> FindWinnerAsync(FindWinnerRequest request);
		Task<IsTileOpenResponse> IsTileOpenAsync(IsTileOpenRequest request);
		Task<InitializeGameResponse> InitializeGameAsync(InitializeGameRequest request);
		Task<PlayTurnResponse> PlayTurnAsync(PlayTurnRequest request);

	}
}