using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using CustomRestServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace CustomRestServer.Authentication
{
    public class OAuthAppProvider : OAuthAuthorizationServerProvider
    {
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                System.Diagnostics.Debug.WriteLine("In OAuthAppProvider");
                var username = context.UserName;
                var password = context.Password;
                User user = UserService.GetUserByCredentials(username, password);
                if (user != null)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Email, user.Email)
                    };
                    ClaimsIdentity oAutIdentity = new ClaimsIdentity(claims, Startup.OAuthOptions.AuthenticationType);
                    context.Validated(new AuthenticationTicket(oAutIdentity, new AuthenticationProperties() { }));
                }
                else
                {
                    context.SetError("invalid_grant", "Error");
                }
            });
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                System.Diagnostics.Debug.WriteLine("In OAuthAppProvider");
                var username = context.Parameters["UserName"];
                var password = context.Parameters["Password"];
                User user = UserService.GetUserByCredentials(username, password);
                if (user != null)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Email, user.Email)
                    };
                    var oAuthIdentity = new ClaimsIdentity(Startup.OAuthOptions.AuthenticationType);
                    oAuthIdentity.AddClaims(claims);
                    context.Validated();
                }
                else
                {
                    context.SetError("invalid_grant", "There was no user matching the entered credentials");
                }
            });
        }
    }
}