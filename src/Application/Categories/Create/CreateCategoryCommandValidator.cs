using Domain.Category;
using FluentValidation;

namespace Application.Categories.Create;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage(CategoryErrors.NameNotEmpty().Description).WithErrorCode(CategoryErrors.NameNotEmpty().Code);
        RuleFor(c => c.MinimumStockQuantity).GreaterThan(0).WithMessage(CategoryErrors.MinimumStockQuantityInvalid().Description).WithErrorCode(CategoryErrors.NameNotEmpty().Code);
    }
}
