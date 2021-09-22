using GerenciamentoComercio_Domain.Utils.IRepository;
using GerenciamentoComercio_Infra.Models;
using System.Collections.Generic;

namespace GerenciamentoComercio_Domain.v1.Interfaces.Repositories
{
    public interface IClientTransactionRepository : IRepository<ClientTransaction>
    {
        List<ClientTransaction> GetTransactionByClient(int clientId);
        List<ClientTransaction> GetTransactionByEmployee(int employeeId);
        bool CheckIfServiceAlreadyExistInTransaction(int id);
        bool CheckIfProductAlreadyExistInTransaction(int id);
    }
}
