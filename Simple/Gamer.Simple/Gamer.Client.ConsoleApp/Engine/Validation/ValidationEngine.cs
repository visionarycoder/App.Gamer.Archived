using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamer.Client.ConsoleApp.Engine.Validation
{
    internal class ValidationEngine
    {
    }

    public interface IInputValidatingEngine
    {
        ValidationResult ValidateNotEmptyInput(string input);
        ValidationResult ValidateInputInCollection(string input, List<string> collection, bool caseSensitive = false);
    }

    internal class InputValidatingEngine : IInputValidatingEngine
    {

        public static readonly string NotEmptyInputErrorMessage = "Empty input";
        public static readonly string InputInCollectionErrorMessage = "Unable to match input.";

        public ValidationResult ValidateNotEmptyInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return new ValidationResult(NotEmptyInputErrorMessage);
            return ValidationResult.Success;
        }

        public ValidationResult ValidateInputInCollection(string input, List<string> collection, bool caseSensitive = false)
        {
            if (caseSensitive)
            {
                input = input.ToLowerInvariant();
                collection = collection.Select(i => i.ToLowerInvariant()).ToList();
            }

            return collection.Contains(input)
                ? ValidationResult.Success
                : new ValidationResult(InputInCollectionErrorMessage);

        }



    }
