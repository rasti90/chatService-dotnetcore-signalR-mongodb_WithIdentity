using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API
{
    public class DefaultClientTokenClaims : ICustomTokenRequestValidator
    {
        public Task ValidateAsync(CustomTokenRequestValidationContext context)
        {
            var userExternalId = context.Result.ValidatedRequest.Raw.Get("userExternalId");
            var aPIKey= context.Result.ValidatedRequest.Raw.Get("client_secret");
            var fullname = context.Result.ValidatedRequest.Raw.Get("fullname");
            if (!(string.IsNullOrWhiteSpace(userExternalId) || string.IsNullOrWhiteSpace(aPIKey) || string.IsNullOrWhiteSpace(fullname))){
                context.Result.ValidatedRequest.ClientClaims.Add(new System.Security.Claims.Claim("userExternalId", userExternalId));
                context.Result.ValidatedRequest.ClientClaims.Add(new System.Security.Claims.Claim("APIKey", aPIKey));
                context.Result.ValidatedRequest.ClientClaims.Add(new System.Security.Claims.Claim("fullname", fullname));
            }
            else
            {
                context.Result.IsError = true;
                context.Result.Error = "Bad Request Parameters";
            }
            
            return Task.CompletedTask;
        }
    }
}
