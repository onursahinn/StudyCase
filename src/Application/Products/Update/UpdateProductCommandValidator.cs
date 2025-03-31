using Domain.Product;
using FluentValidation;
namespace Application.Products.Update;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(c => c.ProductId).NotEmpty().WithMessage(ProductErrors.IdNotEmpty().Description);
        RuleFor(c => c.Title).NotEmpty().WithMessage(ProductErrors.TitleNotEmpty().Description);
        RuleFor(c => c.Title).MinimumLength(1).MaximumLength(200).WithMessage(ProductErrors.TitleLength().Description);
        RuleFor(c => c.CategoryId).NotEmpty().WithMessage(ProductErrors.CategoryNotEmpty().Description);
    }
}
