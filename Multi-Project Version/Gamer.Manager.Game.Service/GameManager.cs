using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Threading.Tasks;
using Gamer.Access.GameDefinition.Interface;
using Gamer.Access.GameSession.Interface;
using Gamer.Access.Player.Interface;
using Gamer.Access.Tile.Interface;
using Gamer.Engine.GameBoard.Interface;
using Gamer.Engine.GamePlay.Interface;
using Gamer.Engine.Validation.Interface;
using Gamer.Framework;
using Gamer.Manager.Game.Interface;
using Gamer.Manager.Game.Service.Helpers;

namespace Gamer.Manager.Game.Service
{

	public class GameManager : ServiceBase<GameManager>, IGameManager
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

		public async Task<Interface.GameDefinition[]> GetGames()
		{
			var gameDefinitions = await gameDefinitionAccess.GetGameDefinitions();
			return gameDefinitions.Convert();
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

		public async Task<Interface.Player> GetCurrentPlayer(Guid gameSessionId)
		{
			
			var gameSession = await gameSessionAccess.GetGameSession(gameSessionId);
			var player = await playerAccess.GetPlayer(gameSession.CurrentPlayerId);
			return player.Convert();

		}

		public async Task<Interface.Player> FindWinner(Guid gameSessionId)
		{
			
			var player = await gamePlayEngine.FindWinner(gameSessionId);
			return player.Convert();

		}

	}

}