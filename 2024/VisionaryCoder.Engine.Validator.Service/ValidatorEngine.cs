using System.ComponentModel.DataAnnotations;
using VisionaryCoder.Engine.Validator.Contract;

namespace VisionaryCoder.Engine.Validator.Service;

public class ValidatorEngine : IValidatorEngine
{


    private readonly IServiceProvider serviceProvider;

    public ValidatorEngine(IServiceProvider serviceProvider)
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
        var temp = Validator.TryValidateObject(instance, context, validationResults: validationResults, validateAllProperties: true);
        return await Task.FromResult(result);

    }

}