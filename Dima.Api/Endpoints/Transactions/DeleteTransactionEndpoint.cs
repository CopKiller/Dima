using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.Transactions;

public class DeleteTransactionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder builder)
        => builder.MapDelete("/", HandleAsync)
            .WithName("Transactions: Delete")
            .WithSummary("Remove uma transação.")
            .WithDescription("Remove uma transação existente, e persiste no banco de dados.")
            .WithOrder(3)
            .Produces <Response<Transaction?>>();


    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ITransactionHandler handler,
        long id)
    {
        var request = new DeleteTransactionRequest()
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id
        };
        
        var result = await handler.DeleteAsync(request);
        return result.IsSuccess 
            ? TypedResults.Ok(result) 
            : TypedResults.BadRequest(result.Data);
    }
}