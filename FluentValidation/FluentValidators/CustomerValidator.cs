using FluentValidation;
using FluentValidationApp.Models;

namespace FluentValidationApp.FluentValidators
{
    public class CustomerValidator:AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("İsim alanı boş olamaz");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email Alanı boş olamaz").EmailAddress().WithMessage("Email alanı doğru formatta olmalıdır.");
            RuleFor(x => x.Age).NotEmpty().WithMessage("Yaş Alanı boş olamaz").InclusiveBetween(18, 60);
        }
    }
}