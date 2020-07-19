using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ChatServer.API.Helper;
using ChatServer.API.Repository.Contract;
using ChatServer.API.Service.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ChatServer.API.Middleware {
    public class ClaimsCheckingMiddleware {
        private readonly RequestDelegate _next;
        public static readonly object HttpContextItemsMiddlewareUserKey = new Object();
        private readonly ILogger _logger;
        private readonly IAuthenticationService _authenticationService;
        
        public ClaimsCheckingMiddleware (RequestDelegate next, ILogger<ClaimsCheckingMiddleware> logger
            , IAuthenticationService authenticationService) {
            if (next == null) {
                throw new ArgumentNullException (nameof (next));
            }
            _next = next;
            _logger = logger;
            _authenticationService = authenticationService;
        }

        public async Task InvokeAsync (HttpContext context, IApplicationRepository applicationRepository, IUserRepository userRepository) {
            var user = context.User as ClaimsPrincipal;
            var claimValues = GetClaimValues(user);

            var userInfo = await _authenticationService.Authenticate(new Model.ViewModels.AuthenticateVM()
            {
                APIKey = claimValues.APIKey,
                UserExternalId = claimValues.userExternalId,
                Fullname = claimValues.userFullname,
                ClientId = claimValues.clientId
            });
         
            if (userInfo != null) {
                context.Items[HttpContextItemsMiddlewareUserKey]=userInfo;
                await _next (context);
            }
              
        }

        private (string APIKey, string clientId, string userExternalId, string userFullname) GetClaimValues(ClaimsPrincipal user)
        {
            return (user.GetClaimValue("client_APIKey"), user.GetClaimValue("client_id")
                , user.GetClaimValue("client_userExternalId"), user.GetClaimValue("client_fullname"));
        }
        
    }
}