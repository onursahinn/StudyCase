using Application.Categories.Create;
using Application.Categories.GetById;
using Application.Categories.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Categories;

public class Update : IEndpoint
{
    public sealed class UpdateCategoryRequest
    {
        public string Name { get; set; }
        public int MinimumStockQuantity { get; set; }
        public bool IsActive { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("categories",
                async (Guid id, [FromBody] UpdateCategoryRequest request, ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    var command = new UpdateCategoryCommand()
                    {
                        CategoryId = id,
                        Name = request.Name,
                        MinimumStockQuantity = request.MinimumStockQuantity,
                        IsActive = request.IsActive
                    };

                    Result<CategoryResponse> result = await sender.Send(command, cancellationToken);

                    return result.Match(Results.Ok, CustomResults.Problem);
                })
            .WithTags(Tags.Categories)
            .RequireAuthorization();
    }
}
