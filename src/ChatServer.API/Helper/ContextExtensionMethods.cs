using System.Threading.Tasks;
using ChatServer.API.Middleware;
using ChatServer.API.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

namespace ChatServer.API.Helper {
    public static class ContextExtensionMethods {
        public static  User GetUserInfo (this HttpContext context) {
            context.Items
                .TryGetValue(ClaimsCheckingMiddleware.HttpContextItemsMiddlewareUserKey, 
                    out var userInfo);
            return (User)userInfo;
        }
        
        public static async Task<User> GetUserInfoAsync (this HttpContext context) {
            context.Items
                .TryGetValue(ClaimsCheckingMiddleware.HttpContextItemsMiddlewareUserKey, 
                    out var userInfo);
            return await Task.FromResult((User)userInfo);
        }

    }
}