using GerenciamentoComercio_Domain.DTOs.Auth;
using GerenciamentoComercio_Domain.Utils.APIMessage;
using System.Threading.Tasks;

namespace GerenciamentoComercio_Domain.v1.Interfaces.Services
{
    public interface IAuthServices
    {
        APIMessage Login(LoginRequest request);
        APIMessage GetTokenRecoverPassword(string userEmail);
        Task<APIMessage> ReadTokenRecoverPassword(string token);
        Task<APIMessage> RecoverPassword(string token, string newPassword);
    }
}
