using System.ComponentModel.DataAnnotations;

namespace VisionaryCoder.Engine.Validator.Contract;

public interface IValidatorEngine
{
    Task<ValidationResult> Validate(object instance);
}