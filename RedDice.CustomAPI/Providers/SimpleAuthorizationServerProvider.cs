using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using RedDice.CustomAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace RedDice.CustomAPI.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            using (AuthRepository _repo = new AuthRepository())
            {

                IdentityUser user = await _repo.FindUser(context.UserName);

                if (user == null)
                {
                    context.SetError("identifier", "The user name is incorrect.");
                    return;
                }

                var password = await _repo.CheckPassword(context.UserName, context.Password);
                if (!password)
                {
                    context.SetError("password", "The password is incorrect.");
                    return;
                }

                //if (user == null)
                //{
                //    context.SetError("invalid_grant", "The user name or password is incorrect.");
                //    return;
                //}

                ClaimsIdentity identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("sub", context.UserName));
                identity.AddClaim(new Claim("role", "user"));
                //context.Validated(identity);

                AuthenticationProperties properties = CreateProperties(context.UserName);
                AuthenticationTicket ticket = new AuthenticationTicket(identity, properties);
                context.Validated(ticket);
            }
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }
    }
}