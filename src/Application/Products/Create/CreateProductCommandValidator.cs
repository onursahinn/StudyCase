using Application.Products.Update;
using Domain.Product;
using FluentValidation;

namespace Application.Products.Create;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(c => c.Title).NotEmpty().WithMessage(ProductErrors.TitleNotEmpty().Description).WithErrorCode(ProductErrors.TitleNotEmpty().Code);
        RuleFor(c => c.Title).MinimumLength(1).WithMessage(ProductErrors.TitleLength().Description).WithErrorCode(ProductErrors.TitleLength().Code)
            .MaximumLength(200).WithMessage(ProductErrors.TitleLength().Description).WithErrorCode(ProductErrors.TitleLength().Code);
        RuleFor(c => c.CategoryId).NotEmpty().WithMessage(ProductErrors.CategoryNotEmpty().Description).WithErrorCode(ProductErrors.CategoryNotEmpty().Code);
    }
}
