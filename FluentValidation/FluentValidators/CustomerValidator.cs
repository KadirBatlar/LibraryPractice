using FluentValidation;
using FluentValidationApp.Models;

namespace FluentValidationApp.FluentValidators
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public string NotEmptyMessage { get; } = "{PropertyName} alanı boş olamaz.";
        public CustomerValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(NotEmptyMessage);
            RuleFor(x => x.Email).NotEmpty().WithMessage(NotEmptyMessage).EmailAddress().WithMessage("Email alanı doğru formatta olmalıdır.");
            RuleFor(x => x.Age).NotEmpty().WithMessage(NotEmptyMessage).InclusiveBetween(18, 60);

            //Custom Validator           
            RuleFor(x => x.BirthDate)
                                    .NotEmpty()
                                    .WithMessage(NotEmptyMessage)
                                    .Must(x =>
                                    {
                                        return DateTime.Now.AddYears(-18) <= x;
                                    })
                                    .WithMessage("18 yaşından büyük olmalısınız.");

            //One to Many Validation
            RuleForEach(x=>x.Addresses).SetValidator(new AddressValidator());



        }
    }
}