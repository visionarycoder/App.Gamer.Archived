using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Gamer.Access.GameSession.Interface;
using Gamer.Access.Tile.Interface;
using Gamer.Engine.Validation.Interface;
using Gamer.Framework;
using Gamer.Utility.ServiceMessaging;
using Microsoft.Extensions.Logging;

namespace Gamer.Engine.Validation.Service
{

	public class ValidationEngine : ServiceBase, IValidationEngine
	{

		private const string NoInputFoundError = "No input found.";
		private const string AddressNotFoundError = "Address not found.";
		private const string GameSessionNotFoundError = "Game session not found.";
		private const string AddressAlreadyPlayedError = "Unable to play this space.";

		private readonly IGameSessionAccess gameSessionAccess;
		private readonly ITileAccess tileAccess;

		public ValidationEngine(IGameSessionAccess gameSessionAccess, ITileAccess tileAccess, ILogger logger)
		: base(logger)
		{

			this.gameSessionAccess = gameSessionAccess;
			this.tileAccess = tileAccess;

		}

		public async Task<ValidateGamePlayAddressResponse> ValidateAsync(ValidateGamePlayAddressRequest request)
		{

			var response = ServiceMessageFactory<ValidateGamePlayAddressResponse>.CreateFrom(request);

			var gameSessionRequest = ServiceMessageFactory<GetGameSessionRequest>.CreateFrom(request);
			gameSessionRequest.GameSessionId = request.GameSessionId;

			var gameSessionResponse = await gameSessionAccess.GetGameSessionAsync(gameSessionRequest);
			if (gameSessionResponse.HasErrors)
			{
				response.Errors += gameSessionResponse.Errors;
				response.ValidationResult = new ValidationResult(GameSessionNotFoundError);
			}
			else if (gameSessionResponse.GameSession == null)
			{
				response.ValidationResult = new ValidationResult(GameSessionNotFoundError);
			}
			else
			{
				response.ValidationResult = ValidationResult.Success;
			}
			
			return response;

		}

		public async Task<ValidateUserInputResponse> ValidateAsync(ValidateUserInputRequest request)
		{

			var response = ServiceMessageFactory<ValidateUserInputResponse>.CreateFrom(request);

			var cleaned = request.Input.Trim().ToUpperInvariant();
			if (string.IsNullOrWhiteSpace(cleaned))
			{
				response.ValidationResult = new ValidationResult(NoInputFoundError);
				return response;
			}

			var tilesRequest = ServiceMessageFactory<FindTilesRequest>.CreateFrom(request);
			tilesRequest.Filter = tile => tile.GameSessionId == request.GameSessionId;
			var tileResponse = await tileAccess.FindTilesAsync(tilesRequest);
			var targetTile = tileResponse.Tiles.FirstOrDefault(i => i.Address == cleaned);
			if (targetTile == null)
			{
				response.ValidationResult = new ValidationResult(AddressNotFoundError);
				return response;
			}

			response.ValidationResult = targetTile.IsEmpty 
				? ValidationResult.Success 
				: new ValidationResult(AddressAlreadyPlayedError);

			return response;

		}

	}

}