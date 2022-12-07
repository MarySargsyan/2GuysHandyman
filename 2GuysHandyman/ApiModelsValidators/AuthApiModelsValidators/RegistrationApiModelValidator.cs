using _2GuysHandyman.ApiModels;
using FluentValidation;

namespace _2GuysHandyman.ApiModelsValidators
{
    public class RegistrationApiModelValidator : AbstractValidator<RegistrationApiModel>
    {
        public RegistrationApiModelValidator()
        {
            RuleFor(u => u.FirstName)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50)
                .Must(f => f.All(Char.IsLetter) == true);

            RuleFor(u => u.LastName)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50)
                .Must(f => f.All(Char.IsLetter) == true);

            RuleFor(u => u.Email)
                .EmailAddress();

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("Your password cannot be empty")
                .MinimumLength(8).WithMessage("Your password length must be at least 8.")
                .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
                .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");


            RuleFor(u => u.Mobile)
                .NotEmpty()
                .MaximumLength(20)
                .Must(m => m.All( c => Char.IsDigit(c)
                    || c == '+'
                    || c == '('
                    || c == ')'
                    || c == '-'
                    || c == ' '
                    )
                == true
                );

            RuleFor(u => u.Adress)
                .NotEmpty()
                .MaximumLength(50)
                .Must(a => a?.All(c => Char.IsLetterOrDigit(c)
                    || c == '/'
                    || c == ' '
                    )
                == true
                );

        }
    }
}
