using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TicTacToe.Models;

namespace TicTacToe.Engines
{

	public class ValidationEngine
	{

		public const string NoInputFoundError = "No input found.";
		public const string AddressNotFoundError = "Address not found.";
		private readonly List<string> allowedAddresses;

		public ValidationEngine(Board board)
		{
			allowedAddresses = board.Cells.Select(i => i.Address).ToList();
		}

		public ValidationEngine(List<string> allowedAddresses)
		{
			this.allowedAddresses = allowedAddresses;
		}

		public ValidationResult ValidateUserInput(string input)
		{

			var cleaned = input.Trim();
			if ( string.IsNullOrWhiteSpace(cleaned) )
				return new ValidationResult(NoInputFoundError);

			if ( allowedAddresses.Contains(cleaned) )
				return ValidationResult.Success;
			return new ValidationResult(AddressNotFoundError);

		}

	}

}