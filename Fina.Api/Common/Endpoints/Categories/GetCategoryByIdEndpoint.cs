using Fina.Api.Common.Api;
using Fina.Api.Models;
using Fina.Core.Handlers;
using Fina.Core.Requests.Categories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fina.Api.Common.Endpoints.Categories
{
    public class GetCategoryByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder builder)
        => builder.MapGet("/{id}", HandleAsync);
        private static async Task<IResult> HandleAsync(ClaimsPrincipal user, [FromServices] ICategoryHandler handler, Guid id)
        {
            var request = new GetCategoryByIdRequest { Id = id, UserId = user.Identity?.Name ?? string.Empty};
            var result = await handler.GetByIdAsync(request);
            if (result.IsSuccess)
                return TypedResults.Ok(result.Data);
            return TypedResults.BadRequest(result.Data);
        }
    }
}
