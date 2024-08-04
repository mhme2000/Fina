using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Requests.Transactions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fina.Api.Common.Endpoints.Transactions
{
    public class GetTransactionsByPeriodEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder builder)
        => builder.MapGet("/", HandleAsync);
        private static async Task<IResult> HandleAsync(ClaimsPrincipal user, [FromServices] ITransactionHandler handler, [FromBody] GetTransactionsByPeriodRequest request, [FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
        {
            request.UserId = user.Identity?.Name ?? string.Empty;
            request.PageNumber = pageNumber;
            request.PageSize = pageSize;
            request.StartDate = startDate;
            request.EndDate = endDate;
            var result = await handler.GetByPeriodAsync(request);
            if (result.IsSuccess)
                return TypedResults.Ok(result.Data);
            return TypedResults.BadRequest(result.Data);
        }
    }
}
