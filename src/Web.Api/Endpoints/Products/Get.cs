using Application.Products.Get;
using Application.Products.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Products;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("products", async ([FromQuery] int offset, [FromQuery] int limit, [FromQuery] string? search,
                [FromQuery] int? minimumStockQuantity, [FromQuery] int? maximumStockQuantity, ISender sender,
                CancellationToken cancellationToken) =>
            {
                var query = new GetProductsQuery(search, minimumStockQuantity, maximumStockQuantity, limit, offset);

                Result<PaginatedResult<ProductResponse>> result = await sender.Send(query, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Products)
            .RequireAuthorization();
    }
}
