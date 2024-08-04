using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Requests.Categories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fina.Api.Common.Endpoints.Categories
{
    public class CreateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder builder)
        => builder.MapPost("/", HandleAsync);
        private static async Task<IResult> HandleAsync(ClaimsPrincipal user, [FromServices] ICategoryHandler handler, [FromBody] CreateCategoryRequest request)
        {
            request.UserId = user.Identity?.Name ?? string.Empty;
            var result = await handler.CreateAsync(request);
            if (result.IsSuccess)
                return TypedResults.Created($"{result.Data?.Id}", result.Data);
            return TypedResults.BadRequest(result.Data);
        }
    }
}
