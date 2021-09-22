using GerenciamentoComercio_Domain.DTOs.ProductTransaction;
using GerenciamentoComercio_Domain.Utils.APIMessage;
using System.Threading.Tasks;

namespace GerenciamentoComercio_Domain.v1.Interfaces.Services
{
    public interface ITransactionsCommonServices
    {
        APIMessage AddNewProductTransactionAsync(AddNewProductServiceTransactionRequest request, string userName, int clientTransactionId);
        APIMessage AddNewServiceTransaction(AddNewProductServiceTransactionRequest request, string userName, int clientTransactionId);
    }
}
