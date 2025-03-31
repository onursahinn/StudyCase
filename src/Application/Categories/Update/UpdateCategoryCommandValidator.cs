using Domain.Category;
using FluentValidation;

namespace Application.Categories.Update;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage(CategoryErrors.NameNotEmpty().Description).WithErrorCode(CategoryErrors.NameNotEmpty().Code);
        RuleFor(c => c.MinimumStockQuantity).GreaterThan(0).WithMessage(CategoryErrors.MinimumStockQuantityInvalid().Description).WithErrorCode(CategoryErrors.MinimumStockQuantityInvalid().Code);
    }
}
