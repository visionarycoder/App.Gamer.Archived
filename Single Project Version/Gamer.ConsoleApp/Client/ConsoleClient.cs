using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Threading.Tasks;
using Gamer.Component.Access.GameDefinition;
using Gamer.Component.Manager.Game;

namespace Gamer.Client
{

	public class ConsoleClient
	{

		public async Task Run(IGameManager gameManager)
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

			Console.WriteLine(AppConstant.ApplicationName);

			var gameDefinition = await SelectGame(gameManager);
			var playerCount = await SelectPlayerCount(gameDefinition);
			var gameSessionId = await gameManager.StartGame(gameDefinition.Id, playerCount);
			DataTable gameBoard;

			Console.WriteLine($"Starting {gameDefinition.Name}");
			bool isGamePlayable;
			do
			{

				gameBoard = await gameManager.GetBoard(gameSessionId);
				ShowGameBoard(gameBoard);

				var currentPlayer = await gameManager.GetCurrentPlayer(gameSessionId);
				if (currentPlayer.IsMachine)
				{
					await gameManager.AutoPlayTurn(gameSessionId);
				}
				else
				{
					var prompt = await gameManager.GetTurnPrompt(gameSessionId);
					Console.WriteLine($"{prompt} {currentPlayer.Name} ({currentPlayer.GamePiece}).");
					var userInput = GetStringInput();
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
			ShowGameBoard(gameBoard);

			var player = await gameManager.FindWinner(gameSessionId);
			ShowWinner(player.Name, player.GamePiece);

			ShowExit();
		}

		private async Task<GameDefinition> SelectGame(IGameManager gameManager)
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

				var input = GetIntegerInput();
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
			while (true)
			{
				for (var idx = gameDefinition.MinNumberOfPlayers; idx <= gameDefinition.MaxNumberOfPlayers; idx++)
				{
					Console.WriteLine($"[{idx}] {idx} player{(idx == 1 ? "" : "s")}.");
				}
				var input = GetIntegerInput();
				if (input >= gameDefinition.MinNumberOfPlayers && input <= gameDefinition.MaxNumberOfPlayers)
				{
					return await Task.FromResult(input);
				}
				Console.WriteLine($"Invalid input ({input}){Environment.NewLine}");
			}

		}

		private int GetIntegerInput()
		{

			do
			{
				var rawInput = Console.ReadLine();
				var trimmedInput = rawInput?.Trim();
				if (int.TryParse(trimmedInput, out var value))
				{
					return value;
				}
				Console.WriteLine("Invalid input.  Please try again.");
			} while (true);

		}

		private string GetStringInput()
		{

			do
			{
				var rawInput = Console.ReadLine();
				var trimmedInput = rawInput?.Trim().ToUpperInvariant();
				if (!string.IsNullOrWhiteSpace(trimmedInput))
				{
					return trimmedInput;
				}
				Console.WriteLine("Invalid input.  Please try again.");
			} while (true);

		}

		private void ShowGameBoard(DataTable gameBoard)
		{
			Console.WriteLine($" {gameBoard.Columns[0].ColumnName} {gameBoard.Columns[1].ColumnName}   {gameBoard.Columns[2].ColumnName}   {gameBoard.Columns[3].ColumnName}");
			Console.WriteLine($" {gameBoard.Rows[0].ItemArray[0]} { gameBoard.Rows[0].ItemArray[1]} | { gameBoard.Rows[0].ItemArray[2]} | { gameBoard.Rows[0].ItemArray[3]}");
			Console.WriteLine("  -----------");
			Console.WriteLine($" {gameBoard.Rows[1].ItemArray[0]} { gameBoard.Rows[1].ItemArray[1]} | { gameBoard.Rows[1].ItemArray[2]} | { gameBoard.Rows[1].ItemArray[3]}");
			Console.WriteLine("  -----------");
			Console.WriteLine($" {gameBoard.Rows[2].ItemArray[0]} { gameBoard.Rows[2].ItemArray[1]} | { gameBoard.Rows[2].ItemArray[2]} | { gameBoard.Rows[2].ItemArray[3]}");
		}

		private void ShowExit()
		{
			Console.WriteLine("Hit [ENTER] to exit.");
			Console.ReadLine();
		}

		private void ShowWinner(string name, string gamePiece)
		{
			Console.WriteLine($"{name} ({gamePiece}) won!");
		}

	}

}