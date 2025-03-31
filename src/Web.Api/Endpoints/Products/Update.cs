using Application.Products.GetById;
using Application.Products.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Products;

public class Update : IEndpoint
{
    public sealed class UpdateProductRequest
    {
        public bool IsActive { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
        public Guid CategoryId { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("products/{id:guid}",
                async (Guid id,[FromBody] UpdateProductRequest request, ISender sender, CancellationToken cancellationToken) =>
                {
                    var command = new UpdateProductCommand()
                    {
                        ProductId = id,
                        Title = request.Title,
                        Description = request.Description,
                        StockQuantity = request.StockQuantity,
                        CategoryId = request.CategoryId,
                        IsActive = request.IsActive,
                    };

                    Result<ProductResponse> result = await sender.Send(command, cancellationToken);

                    return result.Match(Results.Ok, CustomResults.Problem);
                })
            .WithTags(Tags.Products)
            .RequireAuthorization();
    }
}
