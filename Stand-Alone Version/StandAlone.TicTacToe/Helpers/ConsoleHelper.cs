using System;

namespace TicTacToe.Helpers
{

	public static class ConsoleHelper
	{

		public static int GetIntegerInput()
		{
			do
			{
				int value;
				var rawInput = Console.ReadLine();
				var trimmedInput = rawInput?.Trim();
				if ( int.TryParse(trimmedInput, out value) )
					return value;
				Console.WriteLine("Invalid input.  Please try again.");
			} while ( true );

		}

		public static void ShowExit()
		{
			Console.WriteLine("Hit [ENTER} to exit.");
			Console.ReadLine();
		}

	}

}