using Application.Products.Create;
using Domain.Product;
using FluentValidation.TestHelper;

namespace UnitTests.Product;

public class CreateProductCommandValidatorTests
{
    private readonly CreateProductCommandValidator _validator;

    public CreateProductCommandValidatorTests()
    {
        _validator = new CreateProductCommandValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Title_Is_Empty()
    {
        var command = new CreateProductCommand { Title = string.Empty };
        TestValidationResult<CreateProductCommand>? result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Title)
            .WithErrorMessage(ProductErrors.TitleNotEmpty().Description);
    }

    [Fact]
    public void Should_Have_Error_When_Title_Length_Is_Invalid()
    {
        var command = new CreateProductCommand { Title = new string('a', 201) };
        TestValidationResult<CreateProductCommand>? result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Title)
            .WithErrorMessage(ProductErrors.TitleLength().Description);
    }

    [Fact]
    public void Should_Have_Error_When_CategoryId_Is_Empty()
    {
        var command = new CreateProductCommand { CategoryId = Guid.Empty };
        TestValidationResult<CreateProductCommand>? result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.CategoryId)
            .WithErrorMessage(ProductErrors.CategoryNotEmpty().Description);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new CreateProductCommand
        {
            Title = "Valid Title",
            CategoryId = Guid.NewGuid()
        };
        TestValidationResult<CreateProductCommand>? result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
