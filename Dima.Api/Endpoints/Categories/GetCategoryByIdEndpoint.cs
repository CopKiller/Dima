using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.Categories;

public class GetCategoryByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder builder)
        => builder.MapGet("/{id}", HandleAsync)
            .WithName("Categories: Get by Id")
            .WithSummary("Obtem uma categoria")
            .WithDescription("Recupera uma categoria existente pelo Id")
            .WithOrder(4)
            .Produces <Response<Category?>>();


    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ICategoryHandler handler,
        long id)
    {
        var request = new GetCategoryByIdRequest()
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id
        };
        
        var result = await handler.GetByIdAsync(request);
        return result.IsSuccess 
            ? TypedResults.Ok(result) 
            : TypedResults.BadRequest(result.Data);
    }
}