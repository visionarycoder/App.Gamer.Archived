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
using Player = Gamer.Access.Player.Interface.Player;

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

		//public async Task Play(Guid gameDefinition)
		//{

		//StartGame();

		//// How many players?
		//var idx = OFFSET;
		//var items = AllowedNumberOfPlayers.Select(i => i.ToString()).ToList();
		//Console.WriteLine("How many players? ");
		//foreach (var item in items)
		//    Console.WriteLine($"[{idx++}] {item}");

		//int playerCount;
		//do
		//{
		//    playerCount = ConsoleHelper.GetIntegerInput() - OFFSET;
		//    if (AllowedNumberOfPlayers.Contains(playerCount))
		//        break;
		//} while (true);

		//Console.WriteLine(playerCount + " player(s) selected.");
		//SetPlayerCount(playerCount);

		//while (await gamePlayEngine.IsPlayable())
		//{

		//    var board = boardAccess.GetBoard()

		//    Console.WriteLine(board);
		//    Console.WriteLine(players.CurrentPlayer.Name);

		//    if (players.CurrentPlayer.IsMachine)
		//    {
		//        var address = gamePlayEngine.AutoPlay();
		//        ApplyTurn(address);
		//    }
		//    else
		//    {
		//        var address = "";
		//        while (string.IsNullOrWhiteSpace(address))
		//            address = RequestCellAddress();
		//        ApplyTurn(address);
		//    }

		//    players.IncrementPlayer();

		//}

		//EndGame();
		//ConsoleHelper.ShowExit();
		//}

		//private void ApplyTurn(string address)
		//{

		//    var cell = board.Cells.FirstOrDefault(i => i.Address == address);

		//    if (cell == null)
		//        throw new ArgumentException("Address does not exist");

		//    if (!string.IsNullOrWhiteSpace(cell.GamePiece))
		//        throw new ArgumentException("This cell has already been played.");

		//    cell.GamePiece = players.CurrentPlayer.GamePiece;

		//}

		//private string RequestCellAddress()
		//{

		//    do
		//    {
		//        Console.WriteLine("Please take a turn.");
		//        var address = Console.ReadLine()?.Trim().ToUpperInvariant();
		//        var validationResult = validationEngine.ValidateUserInput(address);
		//        if (validationResult == ValidationResult.Success)
		//        {
		//            if (gamePlayEngine.IsTileOpen(address))
		//                return address;
		//            validationResult = new ValidationResult("Tile has already been played.");
		//        }
		//        Console.WriteLine(validationResult.ErrorMessage);
		//    } while (true);

		//}

		//private void StartGame()
		//{
		//    Console.WriteLine("Tic-Tac-Toe");
		//}

		//private void EndGame()
		//{

		//    // Show board
		//    Console.WriteLine(board);

		//    // Declare winner
		//    var gamePiece = gamePlayEngine.FindWinnerGamePiece();
		//    var player = players.ByGamePiece(gamePiece);
		//    if (player == null)
		//        Console.WriteLine("It's a draw!");
		//    else
		//        Console.WriteLine(player.Name + " won!");

		//    Console.WriteLine("Thanks for playing.");

		//}

	}

}