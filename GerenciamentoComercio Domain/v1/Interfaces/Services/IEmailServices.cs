using GerenciamentoComercio_Infra.Models;

namespace GerenciamentoComercio_Domain.v1.Interfaces.Services
{
    public interface IEmailServices
    {
        void SendEmailRecoverPassword(Employee user);
    }
}