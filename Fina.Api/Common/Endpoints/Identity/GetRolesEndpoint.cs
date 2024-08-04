using Fina.Api.Common.Api;
using Fina.Api.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Fina.Api.Common.Endpoints.Categories
{
    public class GetRolesEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder builder)
        => builder.MapGet("/roles", Handle).RequireAuthorization();
        private static IResult Handle(ClaimsPrincipal user)
        {
            if (user.Identity is null || !user.Identity.IsAuthenticated)
                return Results.Unauthorized();

            var identity = (ClaimsIdentity)user.Identity;
            var roles = identity.FindAll(identity.RoleClaimType).Select(c => new
            {
                c.Issuer,
                c.OriginalIssuer,
                c.Type,
                c.Value,
                c.ValueType
            });
            return TypedResults.Json(roles);
        }
    }
}
