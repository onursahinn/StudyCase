using Application.Products.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Products;

public class Create : IEndpoint
{
    public sealed class CreateProductRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
        public Guid CategoryId { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("products", async ([FromBody]CreateProductRequest request, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new CreateProductCommand()
                {
                    Title = request.Title,
                    Description = request.Description,
                    StockQuantity = request.StockQuantity,
                    CategoryId = request.CategoryId,
                };

                Result<Guid> result = await sender.Send(command, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Products)
            .RequireAuthorization();
    }
}
