using Microsoft.AspNetCore.Builder;
using ChatServer.API.Middleware;

namespace ChatServer.API.Helper {
    public static class MiddlewareExtensionMethods {
        public static IApplicationBuilder UseClaimChecking (this IApplicationBuilder builder) {
            return builder.UseWhen (context => context.Request.Headers.ContainsKey ("Authorization"),
                myBuilder => myBuilder.UseMiddleware<ClaimsCheckingMiddleware> ());
        }
    }
}