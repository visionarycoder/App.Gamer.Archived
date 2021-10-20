using System;
using System.Data;

namespace Gamer.Framework.Helpers
{

	public static class ConsoleHelper
	{

		public static int GetIntegerInput()
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

		public static string GetStringInput()
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

		public static void ShowGameBoard(DataTable gameBoard)
		{
			Console.WriteLine($" {gameBoard.Columns[0].ColumnName} {gameBoard.Columns[1].ColumnName}   {gameBoard.Columns[2].ColumnName}   {gameBoard.Columns[3].ColumnName}");
			Console.WriteLine($" {gameBoard.Rows[0].ItemArray[0]} { gameBoard.Rows[0].ItemArray[1]} | { gameBoard.Rows[0].ItemArray[2]} | { gameBoard.Rows[0].ItemArray[3]}");
			Console.WriteLine("  -----------");
			Console.WriteLine($" {gameBoard.Rows[1].ItemArray[0]} { gameBoard.Rows[1].ItemArray[1]} | { gameBoard.Rows[1].ItemArray[2]} | { gameBoard.Rows[1].ItemArray[3]}");
			Console.WriteLine("  -----------");
			Console.WriteLine($" {gameBoard.Rows[2].ItemArray[0]} { gameBoard.Rows[2].ItemArray[1]} | { gameBoard.Rows[2].ItemArray[2]} | { gameBoard.Rows[2].ItemArray[3]}");
		}

		public static void ShowExit()
		{
			Console.WriteLine("Hit [ENTER] to exit.");
			Console.ReadLine();
		}

		public static void ShowWinner(string name, string gamePiece)
		{
			Console.WriteLine($"{name} ({gamePiece}) won!");
		}
	}

}