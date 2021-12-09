using System.Threading.Tasks;

using Gamer.Access.GameDefinition.Interface;
using Gamer.Access.GameSession.Interface;
using Gamer.Access.Player.Interface;
using Gamer.Access.Tile.Interface;
using Gamer.Engine.GameBoard.Interface;
using Gamer.Engine.GamePlay.Interface;
using Gamer.Engine.Validation.Interface;
using Gamer.Framework;
using Gamer.Utility.ServiceMessaging;
using Gamer.Manager.Game.Interface;
using Gamer.Manager.Game.Service.Helpers;

using Microsoft.Extensions.Logging;

using FindWinnerRequest = Gamer.Engine.GamePlay.Interface.FindWinnerRequest;
using FindWinnerResponse = Gamer.Manager.Game.Interface.FindWinnerResponse;
using IsGamePlayableRequest = Gamer.Manager.Game.Interface.IsGamePlayableRequest;
using Player = Gamer.Access.Player.Interface.Player;

namespace Gamer.Manager.Game.Service
{

	public class GameManager : ServiceBase, IGameManager
	{

		private readonly IGameDefinitionAccess gameDefinitionAccess;
		private readonly IGameSessionAccess gameSessionAccess;
		private readonly IPlayerAccess playerAccess;

		private readonly IGameBoardEngine gameBoardEngine;
		private readonly IGamePlayEngine gamePlayEngine;
		private readonly IValidationEngine validationEngine;

		public GameManager(
				 IGameDefinitionAccess gameDefinitionAccess
				 , IGameSessionAccess gameSessionAccess
				 , IPlayerAccess playerAccess
				 , IGameBoardEngine gameBoardEngine
				 , IGamePlayEngine gamePlayEngine
				 , IValidationEngine validationEngine
				 , ILogger logger
				 )
		: base(logger)
		{
			this.gameDefinitionAccess = gameDefinitionAccess;
			this.gameSessionAccess = gameSessionAccess;
			this.playerAccess = playerAccess;
			this.gameBoardEngine = gameBoardEngine;
			this.gamePlayEngine = gamePlayEngine;
			this.validationEngine = validationEngine;
		}

		#region Async
		public async Task<StartGameResponse> StartGameAsync(StartGameRequest request)
		{
			var response = ServiceMessageFactory<StartGameResponse>.CreateFrom(request);
			var initializeGameRequest = ServiceMessageFactory<InitializeGameRequest>.CreateFrom(request);
			initializeGameRequest.GameDefinitionId = request.GameDefinitionId;
			initializeGameRequest.NumberOfPlayers = request.PlayerCount;

			var initializeGameResponse = await gamePlayEngine.InitializeGameAsync(initializeGameRequest);
			response.GameSessionId = initializeGameResponse.GameSession.Id;
			return response;
		}

		public async Task<GetBoardResponse> GetBoardAsync(GetBoardRequest request)
		{

			var response = ServiceMessageFactory<GetBoardResponse>.CreateFrom(request);

			var engineRequest = ServiceMessageFactory<GetGameBoardRequest>.CreateFrom(request);
			engineRequest.GameSessionId = request.GameSessionId;

			var engineResponse = await gameBoardEngine.GetBoardAsync(engineRequest);
			response.GameBoard = engineResponse.GameBoard;

			return response;

		}

		public async Task<ConfirmUsableAccessResponse> ConfirmUsableAddressAsync(ConfirmUsableAddressRequest request)
		{
			var response = ServiceMessageFactory<ConfirmUsableAccessResponse>.CreateFrom(request);
			var validationRequest = ServiceMessageFactory<ValidateUserInputRequest>.CreateFrom(request);
			validationRequest.GameSessionId = request.GameSessionId;
			validationRequest.Input = request.Address;
			var validationResponse = await validationEngine.ValidateAsync(validationRequest);
			response.ValidationResult = validationResponse.ValidationResult;
			return response;
		}

		public async Task<GetTurnPromptResponse> GetTurnPromptAsync(GetTurnPromptRequest request)
		{
			var response = ServiceMessageFactory<GetTurnPromptResponse>.CreateFrom(request);
			var gameSessionRequest = ServiceMessageFactory<GetGameSessionRequest>.CreateFrom(request);
			gameSessionRequest.GameSessionId = request.GameSessionId;
			var gameSessionResponse = await gameSessionAccess.GetGameSessionAsync(gameSessionRequest);

			var gameDefinitionRequest = ServiceMessageFactory<GetGameDefinitionRequest>.CreateFrom(request);
			gameDefinitionRequest.GameDefinitionId = gameSessionResponse.GameSession.GameDefinitionId;

			var gameDefinitionResponse = await gameDefinitionAccess.GetGameDefinitionAsync(gameDefinitionRequest);
			response.Prompt = gameDefinitionResponse.GameDefinition.TurnPrompt;
			return response;
		}

		public async Task<GetGamesResponse> GetGamesAsync(GetGamesRequest request)
		{
			var response = ServiceMessageFactory<Interface.GetGamesResponse>.CreateFrom(request);

			var gameDefinitionRequest = ServiceMessageFactory<Access.GameDefinition.Interface.GetGameDefinitionsRequest>.CreateFrom(request);
			var gameDefinitionResponse = await gameDefinitionAccess.GetGameDefinitionsAsync(gameDefinitionRequest);

			response.GameDefinitions = gameDefinitionResponse.GameDefinitions.Convert();

			return response;
		}

		public async Task<ApplyTurnResponse> ApplyTurnAsync(ApplyTurnRequest request)
		{
			var response = ServiceMessageFactory<ApplyTurnResponse>.CreateFrom(request);
			var playerRequest = ServiceMessageFactory<GetPlayerRequest>.CreateFrom(request);
			playerRequest.PlayerId = request.PlayerId;
			var playerResponse = await playerAccess.GetPlayerAsync(playerRequest);

			var playTurnRequest = ServiceMessageFactory<PlayTurnRequest>.CreateFrom(request);
			playTurnRequest.GameSessionId = request.GameSessionId;
			playTurnRequest.PlayerId = request.PlayerId;
			playTurnRequest.Address = request.Address;
			playTurnRequest.IsAutoPlay = playerResponse.Player.IsMachine;
			
			var playTurnResponse = await gamePlayEngine.PlayTurnAsync(playTurnRequest);
			if (playTurnResponse.HasErrors)
			{
				logger.LogError($"{InstanceId}: {playTurnResponse.Errors}");
			}
			return response;
		}

		public async Task<Interface.IsGamePlayableResponse> IsGamePlayableAsync(IsGamePlayableRequest request)
		{

			var response = ServiceMessageFactory<Interface.IsGamePlayableResponse>.CreateFrom(request);

			var engineRequest = ServiceMessageFactory<Engine.GamePlay.Interface.IsGamePlayableRequest>.CreateFrom(request);
			engineRequest.GameSessionId = request.GameSessionId;
			var engineResponse = await gamePlayEngine.IsGamePlayableAsync(engineRequest);
			response.IsPlayable = engineResponse.Value;

			return response;

		}

		public async Task<GetCurrentPlayerResponse> GetCurrentPlayerAsync(GetCurrentPlayerRequest request)
		{

			var response = ServiceMessageFactory<GetCurrentPlayerResponse>.CreateFrom(request);
			var sessionRequest = ServiceMessageFactory<GetGameSessionRequest>.CreateFrom(request);
			sessionRequest.GameSessionId = request.GameSessionId;
			var sessionResponse = await gameSessionAccess.GetGameSessionAsync(sessionRequest);
			var playerRequest = ServiceMessageFactory<GetPlayerRequest>.CreateFrom(request);
			playerRequest.PlayerId = sessionResponse.GameSession.CurrentPlayerId;
			var playerResponse = await playerAccess.GetPlayerAsync(playerRequest);
			response.Player = playerResponse.Player.Convert();
			return response;

		}

		public async Task<FindWinnerResponse> FindWinnerAsync(Interface.FindWinnerRequest request)
		{

			var response = ServiceMessageFactory<FindWinnerResponse>.CreateFrom(request);
			var engineRequest = ServiceMessageFactory<FindWinnerRequest>.CreateFrom(request);
			engineRequest.GameSessionId = request.GameSessionId;
			var engineResponse = await gamePlayEngine.FindWinnerAsync(engineRequest);
			response.Player = engineResponse.Player.Convert();
			return response;

		}

		public async Task<ApplyTurnResponse> AutoPlayTurn(ApplyTurnRequest request)
		{
			var response = ServiceMessageFactory<ApplyTurnResponse>.CreateFrom(request);
			var autoPlayRequest = ServiceMessageFactory<PlayTurnRequest>.CreateFrom(request);
			autoPlayRequest.IsAutoPlay = true;
			autoPlayRequest.Address = request.Address;
			autoPlayRequest.GameSessionId = request.GameSessionId;
			autoPlayRequest.PlayerId = request.PlayerId;
			var autoPlayResponse = await gamePlayEngine.PlayTurnAsync(autoPlayRequest);
			// ToDo: Add logging.
			return response;
		}
		#endregion Async

	}

}