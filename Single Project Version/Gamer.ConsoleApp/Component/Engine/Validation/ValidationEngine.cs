using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Gamer.Component.Access.GameSession;
using Gamer.Component.Access.Tile;

namespace Gamer.Component.Engine.Validation
{

	public class ValidationEngine : IValidationEngine
	{

		private const string NoInputFoundError = "No input found.";
		private const string AddressNotFoundError = "Address not found.";
		private const string GameSessionNotFoundError = "Game session not found.";
		private const string AddressAlreadyPlayedError = "Unable to play this space.";

		private readonly GameSessionAccess gameSessionAccess;
		private readonly TileAccess tileAccess;

		public ValidationEngine(GameSessionAccess gameSessionAccess, TileAccess tileAccess)
		{

			this.gameSessionAccess = gameSessionAccess;
			this.tileAccess = tileAccess;

		}

		public async Task<ValidationResult> ValidateGameSession(Guid gameSessionId)
		{

			var gameSession = await gameSessionAccess.FindGameSession(i => i.Id == gameSessionId);
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