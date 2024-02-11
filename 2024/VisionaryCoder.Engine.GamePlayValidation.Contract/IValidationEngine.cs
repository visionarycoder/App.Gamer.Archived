using System.ComponentModel.DataAnnotations;

namespace VisionaryCoder.Engine.Validator.Contract;

public interface IValidationEngine
{
    Task<ValidationResult> Validate(object instance);
}