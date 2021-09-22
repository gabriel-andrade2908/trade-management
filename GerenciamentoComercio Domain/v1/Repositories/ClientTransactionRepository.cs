using GerenciamentoComercio_Domain.Utils.IRepository;
using GerenciamentoComercio_Domain.v1.Interfaces.Repositories;
using GerenciamentoComercio_Infra.Context;
using GerenciamentoComercio_Infra.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GerenciamentoComercio_Domain.v1.Repositories
{
    public class ClientTransactionRepository : Repository<ClientTransaction>, IClientTransactionRepository 
    {
        public ClientTransactionRepository(GerenciamentoComercioContext context) : base(context)
        {
        }

        public List<ClientTransaction> GetTransactionByClient(int clientId)
        {
            return _context.ClientTransaction
                .Where(x => x.IdClient == clientId).ToList();
        }

        public List<ClientTransaction> GetTransactionByEmployee(int employeeId)
        {
            return _context.ClientTransaction
                .Where(x => x.IdEmployee == employeeId).ToList();
        }

        public bool CheckIfServiceAlreadyExistInTransaction(int id)
        {
            return _context.ClientTransaction.Include(x => x.ClientTransactionService)
                .Any(x => x.ClientTransactionService.Select(x => x.IdService).Contains(id));
        }

        public bool CheckIfProductAlreadyExistInTransaction(int id)
        {
            return _context.ClientTransaction.Include(x => x.ClientTransactionProduct)
                .Any(x => x.ClientTransactionProduct.Select(x => x.IdProduct).Contains(id));
        }
    }
}
