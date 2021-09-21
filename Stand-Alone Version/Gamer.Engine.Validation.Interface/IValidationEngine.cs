using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Gamer.Engine.Validation.Interface
{
	public interface IValidationEngine
	{
		Task<ValidationResult> ValidateUserInput(Guid gameSessionId, string input);
	}
}