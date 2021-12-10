using System.ComponentModel.DataAnnotations;
using Gamer.Utility.ServiceMessaging;

namespace Gamer.Engine.Validation.Interface
{
	public class ValidateGamePlayAddressResponse : ServiceMessageResponse
	{

		public ValidationResult ValidationResult { get; set; }

	}
}