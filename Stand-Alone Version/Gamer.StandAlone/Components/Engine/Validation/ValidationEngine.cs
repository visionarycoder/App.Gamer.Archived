using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Gamer.StandAlone.Components.Access.GameSessions;
using Gamer.StandAlone.Components.Access.Tile;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Gamer.StandAlone.Components.Engine.Validation
{

    public class ValidationEngine : IValidationEngine
    {

		public const string NoInputFoundError = "No input found.";
		public const string AddressNotFoundError = "Address not found.";
        public const string GameSessionNotFoundError = "Game session not found.";
        public const string AddressAlreadyPlayedError = "Unable to play this space.";

        private readonly IGameSessionAccess gameSessionAccess;
        private readonly ITileAccess tileAccess;

        public ValidationEngine(IHost host)
            : this (
                    ActivatorUtilities.CreateInstance<IGameSessionAccess>(host.Services)
                    , ActivatorUtilities.CreateInstance<ITileAccess>(host.Services)
                )
        {

        }

		public ValidationEngine(IGameSessionAccess gameSessionAccess, ITileAccess tileAccess)
        {

            this.gameSessionAccess = gameSessionAccess;
            this.tileAccess = tileAccess;

		}

		public async Task<ValidationResult> ValidateUserInput(Guid gameSessionId, string input)
        {

            var gameSession = gameSessionAccess.FindGameSession(gameSessionId);
            if (gameSession == null)
            {
                return await Task.FromResult(new ValidationResult(GameSessionNotFoundError));
            }

            var cleaned = input.Trim();
            if (string.IsNullOrWhiteSpace(cleaned))
            {
                return await Task.FromResult(new ValidationResult(NoInputFoundError));
            }

            var tiles = await tileAccess.FindTiles(gameSessionId);
            var targetTile = tiles.FirstOrDefault(i => i.Address == cleaned);
            if (targetTile == null)
            {
                return await Task.FromResult(new ValidationResult(AddressNotFoundError));
            }

            if(! targetTile.IsEmpty)
            {
                return await Task.FromResult(new ValidationResult(AddressAlreadyPlayedError));
            }

            return await Task.FromResult(ValidationResult.Success);

        }

    }

}