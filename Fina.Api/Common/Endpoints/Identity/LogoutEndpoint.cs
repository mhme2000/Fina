using Fina.Api.Common.Api;
using Fina.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace Fina.Api.Common.Endpoints.Categories
{
    public class LogoutEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder builder)
        => builder.MapPost("/logout", HandleAsync).RequireAuthorization();
        private static async Task<IResult> HandleAsync(SignInManager<User> signInManager)
        {
            await signInManager.SignOutAsync();
            return Results.Ok();
        }
    }
}
