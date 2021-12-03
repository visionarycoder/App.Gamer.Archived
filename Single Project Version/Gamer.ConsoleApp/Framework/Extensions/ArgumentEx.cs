using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Gamer.Framework.Extensions
{

    public static class Argument
    {

        public static string StringCannotBeEmpty { get; set; } = "Input string cannot be empty.";
        public static string ArgumentCannotBeNegative { get; set; } = "Numeric argument cannot be negative";

        public static T NotNull<T>([NotNull] this T? value, [CallerArgumentExpression("value")] string name = "") where T : class
        {
            return value is null ? throw new ArgumentNullException(name) : value;
        }

        public static string NotNullOrWhiteSpace([NotNull] this string? value, [CallerArgumentExpression("value")] string name = "")
        {
            return string.IsNullOrWhiteSpace(value)
                ? throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, StringCannotBeEmpty, name),
                    name)
                : value;
        }

        public static int NotNegative(this int value, [CallerArgumentExpression("value")] string name = "")
        {
            return value < 0
                ? throw new ArgumentOutOfRangeException(name, value, string.Format(CultureInfo.CurrentCulture, ArgumentCannotBeNegative, name))
                : value;
        }

    }

}
