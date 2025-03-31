using Application.Abstractions.Data;
using Application.Products.Create;
using Domain.Category;
using Moq;
using SharedKernel;

namespace UnitTests.Product;

public class CreateProductCommandHandlerTests
{
    private readonly Mock<IApplicationDbContext> _contextMock;
    private readonly Mock<IDateTimeProvider> _dateTimeProviderMock;
    private readonly CreateProductCommandHandler _handler;

    public CreateProductCommandHandlerTests()
    {
        _contextMock = new Mock<IApplicationDbContext>();
        _dateTimeProviderMock = new Mock<IDateTimeProvider>();
        _handler = new CreateProductCommandHandler(_contextMock.Object, _dateTimeProviderMock.Object);
    }

    [Fact]
    public async Task HandleShouldReturnFailureWhenCategoryNotFound()
    {
        // Arrange
        var command = new CreateProductCommand
        {
            CategoryId = Guid.NewGuid(),
            Description = "Test Description",
            Title = "Test Title",
            StockQuantity = 10
        };

        _contextMock.Setup(x => x.Categories).Returns(new List<Category>().AsQueryable().BuildMockDbSet().Object);

        // Act
        Result<Guid> result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(CategoryErrors.NotFound(command.CategoryId).Description, result.Error.Description);
    }

    [Fact]
    public async Task HandleShouldReturnSuccessWhenProductIsCreated()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var command = new CreateProductCommand
        {
            CategoryId = categoryId,
            Description = "Test Description",
            Title = "Test Title",
            StockQuantity = 10
        };

        var category = new Category
        {
            Id = categoryId,
            MinimumStockQuantity = 5
        };

        _contextMock.Setup(x => x.Categories).Returns(new List<Category> { category }.AsQueryable().BuildMockDbSet().Object);
        _contextMock.Setup(x => x.Products.Add(It.IsAny<Domain.Product.Product>())).Callback<Domain.Product.Product>(product => product.Id = Guid.NewGuid());
        _contextMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));
        _dateTimeProviderMock.Setup(x => x.UtcNow).Returns(DateTime.UtcNow);

        // Act
        Result<Guid> result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotEqual(Guid.Empty, result.Value);
    }
}
