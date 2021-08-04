using System.Collections.Generic;

namespace GerenciamentoComercio_Domain.DTOs.Auth
{
    public class UserTokenResponse
    {
        public IEnumerable<UserClaimsResponse> Claims { get; set; }
    }
}
