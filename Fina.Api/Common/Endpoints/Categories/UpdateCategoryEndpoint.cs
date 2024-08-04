using Fina.Api.Common.Api;
using Fina.Api.Models;
using Fina.Core.Handlers;
using Fina.Core.Requests.Categories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fina.Api.Common.Endpoints.Categories
{
    public class UpdateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder builder)
        => builder.MapPut("/{id}", HandleAsync);
        private static async Task<IResult> HandleAsync(ClaimsPrincipal user, [FromServices] ICategoryHandler handler, [FromBody] UpdateCategoryRequest request, Guid id)
        {
            request.Id = id;
            request.UserId = user.Identity?.Name ?? string.Empty;
            var result = await handler.UpdateAsync(request);
            if (result.IsSuccess)
                return TypedResults.Ok(result.Data);
            return TypedResults.BadRequest(result.Data);
        }
    }
}
