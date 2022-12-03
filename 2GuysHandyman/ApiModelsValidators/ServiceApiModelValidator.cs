using FluentValidation;
using WebAPI.ApiModels;

namespace _2GuysHandyman.ApiModelsValidators
{
    public class ServiceApiModelValidator : AbstractValidator<ServicesApiModel>
    {
        public ServiceApiModelValidator()
        {
            RuleFor(s => s.Price)
                .NotNull()
                .LessThanOrEqualTo(10000)
                .GreaterThanOrEqualTo(10);

            RuleFor(s => s.Name)
                .NotEmpty()
                .MaximumLength(30);
        }
    }
}
