using GerenciamentoComercio_Domain.DTOs;
using GerenciamentoComercio_Domain.Utils.APIMessage;
using System.Threading.Tasks;

namespace GerenciamentoComercio_Domain.v1.Interfaces.Services
{
    public interface IClientTransactionsServices
    {
        APIMessage GetTransacationsByEmployee(int employeeCode);
        APIMessage GetTransactionsByClient(int clientCode);
        APIMessage GetTransactionsByService(int serviceCode);
        APIMessage GetTransactionsByProduct(int productCode);
        APIMessage AddNewClientTransactionAsync(AddNewClientTransactionRequest request, string userName);
        Task<APIMessage> DeleteClientTransactionAsync(int clientTransactionId);
    }
}
