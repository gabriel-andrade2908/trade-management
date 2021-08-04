using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace GerenciamentoComercio_Domain.Utils.IUserApp
{
    public class UserApp : IUserApp
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserApp(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Nome => _httpContextAccessor.HttpContext.User.Identity.Name;

        public IEnumerable<Claim> GetClaimsIdentity(string role)
        {
            return _httpContextAccessor.HttpContext.User.Claims;
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            throw new NotImplementedException();
        }

        public bool IsAuthenticated()
        {
            return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public bool IsInRole(string role)
        {
            return _httpContextAccessor.HttpContext.User.IsInRole(role);
        }

        public int GetUserCode()
        {
            string userCode = _httpContextAccessor.HttpContext.User.GetUserCode();

            if (string.IsNullOrEmpty(userCode))
                return 0;

            return int.Parse(userCode);
        }

        public string GetUserEmail()
            => _httpContextAccessor.HttpContext.User.Claims
                .Where(x => x.Type == "Email")
                .Select(x => x.Value)
                .FirstOrDefault();

        public string GetUserName()
        => _httpContextAccessor.HttpContext.User.Claims
                .Where(x => x.Type == "UserName")
                .Select(x => x.Value)
                .FirstOrDefault();

        public bool UserHasClaim(string type, string value)
            => _httpContextAccessor.HttpContext.User.Claims
                .Where(x => x.Type == type)
                .Where(x => x.Value == value)
                .Select(x => x)
                .Any();
    }

    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserCode(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal == null)
                throw new ArgumentNullException(nameof(claimsPrincipal));

            return claimsPrincipal.FindFirst("UserCode")?.Value;
        }
    }
}
