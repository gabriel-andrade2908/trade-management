using GerenciamentoComercio_Domain.DTOs.Auth;
using GerenciamentoComercio_Domain.Utils.APIMessage;

namespace GerenciamentoComercio_Domain.v1.Interfaces.Services
{
    public interface IAuthServices
    {
        APIMessage Login(LoginRequest request);
    }
}
