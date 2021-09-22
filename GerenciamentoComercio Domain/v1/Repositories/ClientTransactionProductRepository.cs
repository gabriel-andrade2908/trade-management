using GerenciamentoComercio_Domain.Utils.IRepository;
using GerenciamentoComercio_Domain.v1.Interfaces.Repositories;
using GerenciamentoComercio_Infra.Context;
using GerenciamentoComercio_Infra.Models;
using System.Collections.Generic;
using System.Linq;

namespace GerenciamentoComercio_Domain.v1.Repositories
{
    public class ClientTransactionProductRepository : Repository<ClientTransactionProduct>, IClientTransactionProductRepository 
    {
        public ClientTransactionProductRepository(GerenciamentoComercioContext context) : base(context)
        {
        }

        public List<ClientTransactionProduct> GetTransactionByProduct(int productId)
        {
            return _context.ClientTransactionProduct
                .Where(x => x.IdProduct == productId).ToList();
        }

        public List<ClientTransactionProduct> GetTransactionByClientTransaction(int clientTransaction)
        {
            return _context.ClientTransactionProduct
                .Where(x => x.IdClientTransaction == clientTransaction).ToList();
        }
    }
}
