using System.Text.RegularExpressions;
using FluentValidation;

namespace Shared.Validations;

public static class ValidationRule
{
    public static IRuleBuilderOptions<T, string> PhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder) {

        return ruleBuilder.Must( value => Regex.Match(value, @"^(\d{10})$").Success );
    }
}