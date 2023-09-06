using FluentValidation;
using Oasis.Data.Entities;


namespace Oasis.BL.Validators
{
    public class ToDoValidator : AbstractValidator<ToDo>
    {
        public ToDoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("The Title must not be empty");
            RuleFor(x => x.Title).MinimumLength(4).WithMessage("The Title should consist of more than 3 letter");
            RuleFor(x => x.Title).MaximumLength(200).WithMessage("The Title should not exceed 200 characters in length.");
        }
    }
}
