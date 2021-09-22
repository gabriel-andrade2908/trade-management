using GerenciamentoComercio_Domain.Utils.IRepository;
using GerenciamentoComercio_Domain.v1.Interfaces.Repositories;
using GerenciamentoComercio_Infra.Context;
using GerenciamentoComercio_Infra.Models;
using System.Collections.Generic;
using System.Linq;

namespace GerenciamentoComercio_Domain.v1.Repositories
{
    public class ClientTransactionServiceRepository : Repository<ClientTransactionService>, IClientTransactionServiceRepository
    {
        public ClientTransactionServiceRepository(GerenciamentoComercioContext context) : base(context)
        {
        }

        public List<ClientTransactionService> GetTransactionByService(int serviceId)
        {
            return _context.ClientTransactionService
                .Where(x => x.IdService == serviceId).ToList();
        }

        public List<ClientTransactionService> GetTransactionByClientTransaction(int clientTransaction)
        {
            return _context.ClientTransactionService
                .Where(x => x.IdClientTransaction == clientTransaction).ToList();
        }
    }
}
