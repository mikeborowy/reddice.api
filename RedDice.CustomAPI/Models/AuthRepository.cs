using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace RedDice.CustomAPI.Models
{
    public class AuthRepository : IDisposable
    {
        private AuthDbContext _ctx;

        private UserManager<IdentityUser> _userManager;

        public AuthRepository()
        {
            _ctx = new AuthDbContext();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
        }

        public UserManager<IdentityUser> GetCurrentUserManager()
        {
            return _userManager;
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = userModel.UserName
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public async Task<IdentityUser> FindUser(string userName)
        {
            IdentityUser user = await _userManager.FindByNameAsync(userName);

            return user;
        }

        public async Task<bool> CheckPassword(string userName, string password)
        {
            IdentityUser user = await _userManager.FindByNameAsync(userName);
            var result = await _userManager.CheckPasswordAsync(user, password);

            return result;
        }

        public async Task<IdentityUser> FindUserAndPAssword(string userName, string password)
        {
            IdentityUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(string userName)
        {
            IdentityUser user = await _userManager.FindByNameAsync(userName);

            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await _userManager.CreateIdentityAsync(user, OAuthDefaults.AuthenticationType);
            // Add custom user claims here
            return userIdentity;
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }
    }
}