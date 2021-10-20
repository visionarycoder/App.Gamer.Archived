using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Gamer.Component.Engine.Validation
{
	public interface IValidationEngine
	{
		Task<ValidationResult> ValidateUserInput(Guid gameSessionId, string input);
	}
}