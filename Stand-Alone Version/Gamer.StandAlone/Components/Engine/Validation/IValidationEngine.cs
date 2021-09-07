using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Gamer.StandAlone.Components.Engine.Validation
{
    public interface IValidationEngine
    {
        Task<ValidationResult> ValidateUserInput(Guid gameSessionId, string input);
    }
}