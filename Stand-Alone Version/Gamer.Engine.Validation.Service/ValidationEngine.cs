using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Gamer.Access.GameSession.Interface;
using Gamer.Access.Tile.Interface;
using Gamer.Engine.Validation.Interface;

namespace Gamer.Engine.Validation.Service
{

	public class ValidationEngine : IValidationEngine
	{

		private const string NoInputFoundError = "No input found.";
		private const string AddressNotFoundError = "Address not found.";
		private const string GameSessionNotFoundError = "Game session not found.";
		private const string AddressAlreadyPlayedError = "Unable to play this space.";

		private readonly IGameSessionAccess gameSessionAccess;
		private readonly ITileAccess tileAccess;

		public ValidationEngine(IGameSessionAccess gameSessionAccess, ITileAccess tileAccess)
		{

			this.gameSessionAccess = gameSessionAccess;
			this.tileAccess = tileAccess;

		}

		public async Task<ValidationResult> ValidateGameSession(Guid gameSessionId)
		{

			var gameSession = await gameSessionAccess.GetGameSession(gameSessionId);
			return gameSession == null
					? new ValidationResult(GameSessionNotFoundError)
					: ValidationResult.Success;
		}

		public async Task<ValidationResult> ValidateUserInput(Guid gameSessionId, string input)
		{

			var cleaned = input.Trim().ToUpperInvariant();
			if (string.IsNullOrWhiteSpace(cleaned))
			{
				return await Task.FromResult(new ValidationResult(NoInputFoundError));
			}

			var tiles = await tileAccess.FindTiles(gameSessionId);
			var targetTile = tiles.FirstOrDefault(i => i.Address == cleaned);
			if (targetTile == null)
			{
				return new ValidationResult(AddressNotFoundError);
			}

			var validationResult = targetTile.IsEmpty 
				? ValidationResult.Success 
				: new ValidationResult(AddressAlreadyPlayedError);

			return validationResult;

		}

	}

}