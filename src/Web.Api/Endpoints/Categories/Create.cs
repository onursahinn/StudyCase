using Application.Categories.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Categories;

public class Create : IEndpoint
{
    public sealed class CreateCategoryRequest
    {
        public string Name { get; set; }
        public int MinimumStockQuantity { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("categories",
                async ([FromBody] CreateCategoryRequest request, ISender sender, CancellationToken cancellationToken) =>
                {
                    var command = new CreateCategoryCommand()
                    {
                        Name = request.Name,
                        MinimumStockQuantity = request.MinimumStockQuantity,
                    };

                    Result<Guid> result = await sender.Send(command, cancellationToken);

                    return result.Match(Results.Ok, CustomResults.Problem);
                })
            .WithTags(Tags.Categories)
            .RequireAuthorization();
    }
}
