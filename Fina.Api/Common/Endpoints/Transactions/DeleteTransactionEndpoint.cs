using Fina.Api.Common.Api;
using Fina.Api.Models;
using Fina.Core.Handlers;
using Fina.Core.Requests.Transactions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fina.Api.Common.Endpoints.Transactions
{
    public class DeleteTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder builder)
        => builder.MapDelete("/{id}", HandleAsync);
        private static async Task<IResult> HandleAsync(ClaimsPrincipal user, [FromServices] ITransactionHandler handler, Guid id)
        {
            var request = new DeleteTransactionRequest
            {
                Id = id,
                UserId = user.Identity?.Name ?? string.Empty
            };
            var result = await handler.DeleteAsync(request);
            if (result.IsSuccess)
                return TypedResults.Ok(result.Data);
            return TypedResults.BadRequest(result.Data);
        }
    }
}
