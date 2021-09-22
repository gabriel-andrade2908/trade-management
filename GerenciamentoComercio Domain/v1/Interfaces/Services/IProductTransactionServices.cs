using GerenciamentoComercio_Domain.DTOs.ProductTransaction;
using GerenciamentoComercio_Domain.Utils.APIMessage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerenciamentoComercio_Domain.v1.Interfaces.Services
{
    public interface IProductTransactionServices
    {
        Task<APIMessage> AddNewProductTransactionAsync(List<AddNewProductServiceTransactionRequest> products, int clientTransactionId, string userName);
        Task<APIMessage> UpdateProductTransactionAsync(int quantity, int id);
        Task<APIMessage> DeleteProductTransactionAsync(int id);
    }
}
