using GerenciamentoComercio_Domain.DTOs.ProductTransaction;
using GerenciamentoComercio_Domain.Utils.APIMessage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerenciamentoComercio_Domain.v1.Interfaces.Services
{
    public interface IServiceTransactionServices
    {
        Task<APIMessage> AddNewServiceTransactionAsync(List<AddNewProductServiceTransactionRequest> services, int clientTransactionId, string userName);
        Task<APIMessage> UpdateServiceTransactionAsync(int quantity, int id);
        Task<APIMessage> DeleteServiceTransactionAsync(int id);
    }
}
