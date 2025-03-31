using Application.Products.Update;
using Domain.Product;
using FluentValidation;

namespace Application.Products.Create;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(c => c.Title).NotEmpty().WithMessage(ProductErrors.TitleNotEmpty().Description);
        RuleFor(c => c.Title).MinimumLength(1).MaximumLength(200).WithMessage(ProductErrors.TitleLength().Description);
        RuleFor(c => c.CategoryId).NotEmpty().WithMessage(ProductErrors.CategoryNotEmpty().Description);
    }
}
