using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Threading.Tasks;
using Gamer.Access.GameSession.Interface;
using Gamer.Engine.GamePlay.Interface;
using Gamer.Framework.Helpers;
using Gamer.Framework.ServiceMessaging;
using Gamer.Manager.Game.Interface;
using FindWinnerRequest = Gamer.Engine.GamePlay.Interface.FindWinnerRequest;
using GameSession = Gamer.Access.GameSession.Interface.GameSession;

namespace Gamer.Client.ConsoleAppAsync
{

	public class ConsoleClient
	{

		private readonly IGameManagerAsync gameManager;

		public ConsoleClient(IGameManagerAsync gameManager)
		{

			this.gameManager = gameManager;

		}

		public async Task Run()
		{

			//
			// Select Game
			// Initialize players
			// Run Game - Loop
			//   Get Turn Input
			//   ValidateAsync Input
			//   Apply Input
			//   Check Game Status
			//   If Finished
			//     Exit Loop
			// Declare Winner
			//

			// Setup game.
			var gameDefinition = await SelectGame();
			var playerCount = await SelectPlayerCount(gameDefinition);

			// This becomes the root message for all that follow.  
			// All correlation ids will link back to this message.
			var request = ServiceMessageFactory<StartGameRequest>.Create();
			request.GameDefinitionId = gameDefinition.Id;
			request.PlayerCount = playerCount;
			
			// Start a game
			var gameSessionResponse = await gameManager.StartGameAsync(request);
			
			Console.WriteLine($"Starting {gameDefinition.Name}");

			var boardRequest = ServiceMessageFactory<GetBoardRequest>.CreateFrom(request);
			boardRequest.GameSessionId = gameSessionResponse.GameSessionId;

			var boardResponse = await gameManager.GetBoardAsync(boardRequest);
			ConsoleHelper.ShowGameBoard(boardResponse.GameBoard);

			bool isGamePlayable;
			do
			{

				var currentPlayerRequest = ServiceMessageFactory<GetCurrentPlayerRequest>.CreateFrom(request);
				currentPlayerRequest.GameSessionId = gameSessionResponse.GameSessionId;
				var currentPlayerResponse = await gameManager.GetCurrentPlayerAsync(currentPlayerRequest);
				if (currentPlayerResponse.Player.IsMachine)
				{
					var playTurnRequest = ServiceMessageFactory<PlayTurnRequest>.CreateFrom(request);
					playTurnRequest.PlayerId = currentPlayerResponse.Player.Id;
					playTurnRequest.GameSessionId = gameSessionResponse.GameSessionId;
					var playTurnResponse = await gameManager.AutoPlayTurnAsync(playTurnRequest);
				}
				else
				{
					var prompt = gameSessionResponse.
					Console.WriteLine($"{prompt} {currentPlayer.Name} ({currentPlayer.GamePiece}).");
					var userInput = ConsoleHelper.GetStringInput();
					var validationResult = await gameManager.ConfirmUsableAddressAsync(gameSessionId, userInput);
					if (validationResult == ValidationResult.Success)
					{
						await gameManager.ApplyTurnAsync(gameSessionId, currentPlayer.Id, userInput);
					}
					else
					{
						Console.WriteLine($"{validationResult.ErrorMessage}.  Please try again.");
					}
				}
				isGamePlayable = await gameManager.IsGamePlayableAsync(gameSessionId);

			} while (isGamePlayable);

			boardRequest = ServiceMessageFactory<GetBoardRequest>.CreateFrom(request);
			boardRequest.GameSessionId = gameSessionResponse.GameSessionId;
			boardResponse = await gameManager.GetBoardAsync(boardRequest);
			ConsoleHelper.ShowGameBoard(boardResponse.GameBoard);

			var findWinnerRequest = ServiceMessageFactory<FindWinnerRequest>.CreateFrom(request);
			findWinnerRequest.GameSessionId = gameSessionResponse.GameSessionId;
			var findWinnerResponse = await gameManager.FindWinnerAsync(findWinnerRequest);
			ConsoleHelper.ShowWinner(findWinnerResponse.Player.Name, findWinnerResponse.Player.GamePiece);
			
			ConsoleHelper.ShowExit();

		}
		
		private async Task<GameDefinition> SelectGame()
		{

			var gameDefinitions = gameManager.GetGames();
			while (true)
			{
				Console.WriteLine("Select a game to play.");
				var idx = 1;
				foreach (var gameDefinition in gameDefinitions)
				{
					Console.WriteLine($"[{idx++}] {gameDefinition.Name}");
				}

				var input = ConsoleHelper.GetIntegerInput();
				if (input > 0 && input <= gameDefinitions.Length)
				{
					return gameDefinitions[input - 1]; 
				}
				Console.WriteLine($"Invalid input ({input}){Environment.NewLine}");
			}

		}

		private async Task<int> SelectPlayerCount(GameDefinition gameDefinition)
		{

			Console.WriteLine("How many players?");
			while(true)
			{
				for (var idx = gameDefinition.MinNumberOfPlayers; idx <= gameDefinition.MaxNumberOfPlayers; idx++)
				{
					Console.WriteLine($"[{idx}] {idx} player{(idx == 1 ? "" : "s")}.");
				}
				var input = ConsoleHelper.GetIntegerInput();
				if (input >= gameDefinition.MinNumberOfPlayers && input <= gameDefinition.MaxNumberOfPlayers)
				{
					return await Task.FromResult(input);
				}
				Console.WriteLine($"Invalid input ({input}){Environment.NewLine}");
			}

		}

	}

}
