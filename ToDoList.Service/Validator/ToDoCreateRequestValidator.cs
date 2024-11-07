using FluentValidation;
using ToDoList.Models.Dtos.ToDos.Requests;

namespace ToDoList.Models.Dtos.Validator;

public class ToDoCreateRequestValidator : AbstractValidator<ToDoCreateRequestDto>
{
    public ToDoCreateRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .Length(3, 50).WithMessage("Title must be between 3 and 50 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

        RuleFor(x => x.StartDate)
            .LessThanOrEqualTo(x => x.EndDate).WithMessage("Start Date must be before or equal to End Date.");

        RuleFor(x => x.Priority)
            .IsInEnum().WithMessage("Invalid priority value.");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("CategoryId must be a positive integer.");
    }
}