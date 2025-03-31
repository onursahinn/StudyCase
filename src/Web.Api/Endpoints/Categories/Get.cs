using Application.Categories.Get;
using Application.Categories.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Categories;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("categories", async ([FromQuery] int offset, [FromQuery] int limit, [FromQuery] string? search, 
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var query = new GetCategoriesQuery(search, limit, offset);

                Result<PaginatedResult<CategoryResponse>> result = await sender.Send(query, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Categories)
            .RequireAuthorization();
    }
}
