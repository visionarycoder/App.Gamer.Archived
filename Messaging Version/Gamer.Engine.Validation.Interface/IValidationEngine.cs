using System.Threading.Tasks;
using Gamer.Framework;

namespace Gamer.Engine.Validation.Interface
{

	public interface IValidationEngine : IServiceBase
	{
		
		Task<ValidateUserInputResponse> ValidateAsync(ValidateUserInputRequest request);
		Task<ValidateGamePlayAddressResponse> ValidateAsync(ValidateGamePlayAddressRequest request);
	}

	
}