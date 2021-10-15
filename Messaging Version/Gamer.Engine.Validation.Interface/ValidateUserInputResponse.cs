using System.ComponentModel.DataAnnotations;
using Gamer.Framework.ServiceMessaging;

namespace Gamer.Engine.Validation.Interface
{
	public class ValidateUserInputResponse : ServiceMessageResponse
	{
	
		public ValidationResult ValidationResult { get; set; }

	}
}