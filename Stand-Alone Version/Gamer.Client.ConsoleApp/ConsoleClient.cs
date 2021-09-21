using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Threading.Tasks;
using Gamer.Framework.Helpers;
using Gamer.Manager.Game.Interface;
using Gamer.Manager.Game.Service;

namespace Gamer.Client.ConsoleApp
{

	public class ConsoleClient
	{

		private readonly IGameManager gameManager;

		public ConsoleClient(IGameManager gameManager)
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
			//   Validate Input
			//   Apply Input
			//   Check Game Status
			//   If Finished
			//     Exit Loop
			// Declare Winner
			//

			var gameDefinition = await SelectGame();
			var playerCount = await SelectPlayerCount(gameDefinition);
			var gameSessionId = await gameManager.StartGame(gameDefinition.Id, playerCount);
			DataTable gameBoard;

			Console.WriteLine($"Starting {gameDefinition.Name}");
			var isGamePlayable = true;
			do
			{

				gameBoard = await gameManager.GetBoard(gameSessionId);
				ConsoleHelper.ShowGameBoard(gameBoard);

				var currentPlayer = await gameManager.GetCurrentPlayer(gameSessionId);
				if (currentPlayer.IsMachine)
				{
					await gameManager.AutoPlayTurn(gameSessionId);
				}
				else
				{
					var prompt = await gameManager.GetTurnPrompt(gameSessionId);
					Console.WriteLine($"{prompt} {currentPlayer.Name} ({currentPlayer.GamePiece}).");
					var userInput = ConsoleHelper.GetStringInput();
					var validationResult = await gameManager.ConfirmUsableAddress(gameSessionId, userInput);
					if (validationResult == ValidationResult.Success)
					{
						await gameManager.ApplyTurn(gameSessionId, currentPlayer.Id, userInput);
					}
					else
					{
						Console.WriteLine($"{validationResult.ErrorMessage}.  Please try again.");
					}
				}
				isGamePlayable = await gameManager.IsGamePlayable(gameSessionId);

			} while (isGamePlayable);

			gameBoard = await gameManager.GetBoard(gameSessionId);
			ConsoleHelper.ShowGameBoard(gameBoard);

			var player = await gameManager.FindWinner(gameSessionId);
			ConsoleHelper.ShowWinner(player.Name, player.GamePiece);
			
			ConsoleHelper.ShowExit();

		}
		
		private async Task<GameDefinition> SelectGame()
		{

			var gameDefinitions = await gameManager.GetGames();
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
