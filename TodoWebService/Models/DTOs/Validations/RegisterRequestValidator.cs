using FluentValidation;
using System.Text.RegularExpressions;
using TodoWebService.Models.DTOs.Auth;

namespace TodoWebService.Models.DTOs.Validations
{
    public static class SharedValidator
    {
        public static bool BeValidPassword(string password)
        {
            return new Regex(@"\d").IsMatch(password)
                && new Regex(@"[a-z]").IsMatch(password)
                && new Regex(@"[A-Z]").IsMatch(password)
                && new Regex(@"[\.,';]").IsMatch(password);
        }
    }

    //public static class ValidationRulesExtension
    //{
    //    public static IRuleBuilderOptions<T, string> Password<T>(
    //        this IRuleBuilder<T, string> ruleBuilder,
    //        bool mustContainLowerCase = true,
    //        bool mustContainUpperCase = true,
    //        bool mustContainDigit = true
    //        )
    //    {
    //        // yoxlamalarla oz methodunuzu yarada bilersiz
    //        return null;
    //    }
    //}

    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(e => e.Email).EmailAddress().NotEmpty();
            RuleFor(e => e.Password).Must(SharedValidator.BeValidPassword).NotEmpty();
        }
    }
}
