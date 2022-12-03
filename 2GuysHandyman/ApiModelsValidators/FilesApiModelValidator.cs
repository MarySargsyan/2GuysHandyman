using _2GuysHandyman.ApiModels;
using FluentValidation;

namespace _2GuysHandyman.ApiModelsValidators
{
    public class FilesApiModelValidator : AbstractValidator<FilesApiModel>
    {
        public FilesApiModelValidator()
        {
            RuleFor(x => x.FileLink)
                .NotEmpty();
        }
    }
}
