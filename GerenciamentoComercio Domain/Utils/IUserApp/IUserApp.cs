using System.Security.Claims;
using System.Collections.Generic;

namespace GerenciamentoComercio_Domain.Utils.IUserApp
{
    public interface IUserApp
    {
        string Nome { get; }
        int GetUserCode();
        string GetUserName();
        string GetUserEmail();
        bool IsAuthenticated();
        bool IsInRole(string role);
        IEnumerable<Claim> GetClaimsIdentity();
        bool UserHasClaim(string type, string value);
        string GetIfUserIsAdmin();
    }
}
