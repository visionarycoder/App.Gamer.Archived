using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TicTacToe.Access.GameDefinition;
using TicTacToe.Access.GameSessions;
using TicTacToe.Access.Player;
using TicTacToe.Engine.GamePlay;
using TicTacToe.Engine.Validation;
using TicTacToe.Framework.Helpers;

namespace TicTacToe.Manager
{

	public class GameManager
	{

		private const int OFFSET = 0;

        private readonly GameDefinitionAccess gameDefinitionAccess;
        private readonly GameSessionAccess gameSessionAccess;
		private readonly PlayerAccess playerAccess;

        private readonly GamePlayEngine gamePlayEngine;
		private readonly ValidationEngine validationEngine;

		public IList<int> AllowedNumberOfPlayers { get; } = new List<int> { 0, 1, 2 };

		public GameManager()
			: this(new BoardAccess(), new PlayerAccess(), new GamePlayEngine(), new ValidationEngine())
		{
		}

		public GameManager(BoardAccess boardAccess, PlayerAccess playerAccess, GamePlayEngine gamePlayEngine, ValidationEngine validationEngine)
        {
            this.boardAccess = boardAccess;
			this.playerAccess = playerAccess;
			this.gamePlayEngine = gamePlayEngine;
			this.validationEngine = validationEngine;
		}

		public void Play()
		{

			StartGame();

			// How many players?
			var idx = OFFSET;
			var items = AllowedNumberOfPlayers.Select(i => i.ToString()).ToList();
			Console.WriteLine("How many players? ");
			foreach ( var item in items )
				Console.WriteLine($"[{idx++}] {item}");

			int playerCount;
			do
			{
				playerCount = ConsoleHelper.GetIntegerInput() - OFFSET;
				if ( AllowedNumberOfPlayers.Contains(playerCount) )
					break;
			} while ( true );

			Console.WriteLine(playerCount + " player(s) selected.");
			SetPlayerCount(playerCount);

			while ( gamePlayEngine.IsPlayable() )
			{

				var board = boardAccess.GetBoard()
				Console.WriteLine(board);
				Console.WriteLine(players.CurrentPlayer.Name);

				if ( players.CurrentPlayer.IsMachine )
				{
					var address = gamePlayEngine.AutoPlay();
					ApplyTurn(address);
				}
				else
				{
					var address = "";
					while (string.IsNullOrWhiteSpace(address))
						address = RequestCellAddress();
					ApplyTurn(address);
				}

				players.IncrementPlayer();

			}

			EndGame();
			ConsoleHelper.ShowExit();
		}

		private void SetPlayerCount(int playerCount)
		{

			TicTacToePlayer player1;
			TicTacToePlayer player2;

			switch ( playerCount )
			{
				case 0:
					player1 = new TicTacToePlayer("Player 1", "X", true);
					player2 = new TicTacToePlayer("Player 2", "O", true);
					break;
				case 1:
					player1 = new TicTacToePlayer("Player 1", "X");
					player2 = new TicTacToePlayer("Player 2", "O", true);
					break;
				case 2:
					player1 = new TicTacToePlayer("Player 1", "X");
					player2 = new TicTacToePlayer("Player 2", "O");
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			players = new TicTacToePlayers(player1, player2);

		}

		private void ApplyTurn(string address)
		{

			var cell = board.Cells.FirstOrDefault(i => i.Address == address);

			if ( cell == null )
				throw new ArgumentException("Address does not exist");

			if (! string.IsNullOrWhiteSpace(cell.GamePiece))
				throw new ArgumentException("This cell has already been played.");

			cell.GamePiece = players.CurrentPlayer.GamePiece;

		}

		private string RequestCellAddress()
		{

			do
			{
				Console.WriteLine("Please take a turn.");
				var address = Console.ReadLine()?.Trim().ToUpperInvariant();
				var validationResult = validationEngine.ValidateUserInput(address);
				if (validationResult == ValidationResult.Success)
				{
					if(gamePlayEngine.IsCellOpen(address))
						return address;
					validationResult = new ValidationResult("Tile has already been played.");
				}
				Console.WriteLine(validationResult.ErrorMessage);
			} while ( true );

		}

		private void StartGame()
		{
			Console.WriteLine("Tic-Tac-Toe");
		}

		private void EndGame()
		{

			// Show board
			Console.WriteLine(board);

			// Declare winner
			var gamePiece = gamePlayEngine.FindWinnerGamePiece();
			var player = players.ByGamePiece(gamePiece);
			if ( player == null )
				Console.WriteLine("It's a draw!");
			else
				Console.WriteLine(player.Name + " won!");

			Console.WriteLine("Thanks for playing.");

		}

	}

}