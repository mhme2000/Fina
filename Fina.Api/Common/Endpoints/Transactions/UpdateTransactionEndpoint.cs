using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Requests.Transactions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fina.Api.Common.Endpoints.Transactions
{
    public class UpdateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder builder)
        => builder.MapPut("/{id}", HandleAsync);
        private static async Task<IResult> HandleAsync(ClaimsPrincipal user, [FromServices] ITransactionHandler handler, [FromBody] UpdateTransactionByIdRequest request, Guid id)
        {
            request.UserId = user.Identity?.Name ?? string.Empty;
            request.Id = id;
            var result = await handler.UpdateAsync(request);
            if (result.IsSuccess)
                return TypedResults.Ok(result.Data);
            return TypedResults.BadRequest(result.Data);
        }
    }
}
