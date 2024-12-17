using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.Categories;

public class UpdateCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder builder)
        => builder.MapPut("/", HandleAsync)
            .WithName("Categories: Update")
            .WithSummary("Atualiza uma categoria.")
            .WithDescription("Atualiza uma categoria existente, e persiste no banco de dados.")
            .WithOrder(2)
            .Produces <Response<Category?>>();


    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        UpdateCategoryRequest request,
        ICategoryHandler handler,
        long id)
    {
        request.UserId = user.Identity?.Name ?? string.Empty;
        request.Id = id;
        var result = await handler.UpdateAsync(request);
        return result.IsSuccess 
            ? TypedResults.Ok(result) 
            : TypedResults.BadRequest(result.Data);
    }
}