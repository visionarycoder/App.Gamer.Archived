using System.ComponentModel.DataAnnotations;
using VisionaryCoder.Engine.Validator.Contract;

namespace VisionaryCoder.Engine.Validator.Service;

public class ValidationEngine : IValidationEngine
{


    private readonly IServiceProvider serviceProvider;

    public ValidationEngine(IServiceProvider serviceProvider)
    {

    }

    public async Task<ValidationResult> Validate(object instance, IDictionary<object,object?> items)
    {

        var result = ValidationResult.Success;
        if (instance == null)
        {
            return new ValidationResult("Instance is null");
        }


        var context = new ValidationContext(instance, serviceProvider: null, items: items);
        var validationResults = new List<ValidationResult>();
        if (System.ComponentModel.DataAnnotations.Validator.TryValidateObject(instance, context, validationResults: validationResults, validateAllProperties: true))
        {
            return result;
        }

        return await Task.FromResult(result);

    }

    public async Task<ValidationResult> Validate(object instance)
    {
        throw new NotImplementedException();
    }
}