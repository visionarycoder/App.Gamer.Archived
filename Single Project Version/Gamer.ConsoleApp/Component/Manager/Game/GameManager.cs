using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Threading.Tasks;
using Gamer.Component.Access.GameDefinition;
using Gamer.Component.Access.GameSession;
using Gamer.Component.Access.Player;
using Gamer.Component.Access.Tile;
using Gamer.Component.Engine.GameBoard;
using Gamer.Component.Engine.GamePlay;
using Gamer.Component.Engine.Validation;

namespace Gamer.Component.Manager.Game
{

	public class GameManager : IGameManager
	{

		private readonly IGameDefinitionAccess gameDefinitionAccess;
		private readonly IGameSessionAccess gameSessionAccess;
		private readonly IPlayerAccess playerAccess;
		private readonly ITileAccess tileAccess;

		private readonly IGameBoardEngine gameBoardEngine;
		private readonly IGamePlayEngine gamePlayEngine;
		private readonly IValidationEngine validationEngine;

		public GameManager(
				 IGameDefinitionAccess gameDefinitionAccess
				 , IGameSessionAccess gameSessionAccess
				 , IPlayerAccess playerAccess
				 , ITileAccess tileAccess
				 , IGameBoardEngine gameBoardEngine
				 , IGamePlayEngine gamePlayEngine
				 , IValidationEngine validationEngine)
		{
			this.gameDefinitionAccess = gameDefinitionAccess;
			this.gameSessionAccess = gameSessionAccess;
			this.playerAccess = playerAccess;
			this.tileAccess = tileAccess;
			this.gameBoardEngine = gameBoardEngine;
			this.gamePlayEngine = gamePlayEngine;
			this.validationEngine = validationEngine;
		}

		public async Task<GameDefinition[]> GetGames()
		{
			var gameDefinitions = await gameDefinitionAccess.GetGameDefinitions();
			return gameDefinitions;
		}

		public async Task<Guid> StartGame(Guid gameDefinitionId, int numberOfPlayers)
		{
			var gameSession = await gamePlayEngine.InitializeGame(gameDefinitionId, numberOfPlayers);
			return gameSession.Id;
		}

		public async Task<DataTable> GetBoard(Guid gameSessionId)
		{

			return await gameBoardEngine.GetBoard(gameSessionId);

		}

		public async Task AutoPlayTurn(Guid gameSessionId)
		{
			await gamePlayEngine.AutoPlayTurn(gameSessionId);
		}

		public async Task<ValidationResult> ConfirmUsableAddress(Guid gameSessionId, string address)
		{
			var results = await validationEngine.ValidateUserInput(gameSessionId, address);
			return results;
		}

		public async Task<string> GetTurnPrompt(Guid gameSessionId)
		{
			var gameSession = await gameSessionAccess.GetGameSession(gameSessionId);
			var gameDefinition = await gameDefinitionAccess.GetGameDefinition(gameSession.GameDefinitionId);
			return gameDefinition.TurnPrompt;
		}

		public async Task ApplyTurn(Guid gameSessionId, Guid playerId, string address)
		{

			await gamePlayEngine.PlayTurn(gameSessionId, playerId, address);

		}

		public async Task<bool> IsGamePlayable(Guid gameSessionId)
		{

			var isPlayable = await gamePlayEngine.IsPlayable(gameSessionId);
			return isPlayable;

		}

		public async Task<Player> GetCurrentPlayer(Guid gameSessionId)
		{
			
			var gameSession = await gameSessionAccess.GetGameSession(gameSessionId);
			var player = await playerAccess.GetPlayer(gameSession.CurrentPlayerId);
			return player;

		}

		public async Task<Player> FindWinner(Guid gameSessionId)
		{
			
			var player = await gamePlayEngine.FindWinner(gameSessionId);
			return player;

		}

	}

}