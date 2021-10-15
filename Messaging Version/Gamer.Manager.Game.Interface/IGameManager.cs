using System.Threading.Tasks;
using Gamer.Framework;

namespace Gamer.Manager.Game.Interface
{

	public interface IGameManager : IServiceBase
	{

		Task<GetGamesResponse> GetGamesAsync(GetGamesRequest request);
		Task<ApplyTurnResponse> ApplyTurnAsync(ApplyTurnRequest request);
		Task<ConfirmUsableAccessResponse> ConfirmUsableAddressAsync(ConfirmUsableAddressRequest request);
		Task<IsGamePlayableResponse> IsGamePlayableAsync(IsGamePlayableRequest request);
		Task<StartGameResponse> StartGameAsync(StartGameRequest request);
		Task<GetBoardResponse> GetBoardAsync(GetBoardRequest request);
		Task<GetCurrentPlayerResponse> GetCurrentPlayerAsync(GetCurrentPlayerRequest request);
		Task<FindWinnerResponse> FindWinnerAsync(FindWinnerRequest request);

	}

}