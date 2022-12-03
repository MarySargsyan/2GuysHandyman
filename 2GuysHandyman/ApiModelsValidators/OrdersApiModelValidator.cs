using _2GuysHandyman.ApiModels;
using FluentValidation;

namespace _2GuysHandyman.ApiModelsValidators
{
    public class OrdersApiModelValidator : AbstractValidator<OrdersApiModel>
    {
        public OrdersApiModelValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(1000);

            RuleForEach(x => x.Files).SetValidator(new FilesApiModelValidator());
        }
    }
}
