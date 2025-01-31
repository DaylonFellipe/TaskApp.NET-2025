using Daylon.TaskApp.Application.UseCases.Task.Register;
using Daylon.TaskApp.Communication.Requests;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Daylon.TaskApp.Application.UseCases.Task.Update
{
    public class UpdateStringsTaskValidator : AbstractValidator<RequestUpdateTaskJson>
    {
        public UpdateStringsTaskValidator()
        {
            RuleFor(name => name.Name)
                  .NotEmpty().WithMessage("Name is required")
                  .MaximumLength(200).WithMessage("Name must be less than 200 characters");

            RuleFor(description => description.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(1000).WithMessage("Name must be less than 1000 characters");
        }
    }
}
