using GerenciamentoComercio_Domain.Utils.IRepository;
using GerenciamentoComercio_Domain.v1.Interfaces.Repositories;
using GerenciamentoComercio_Infra.Context;
using GerenciamentoComercio_Infra.Models;
using System.Collections.Generic;
using System.Linq;

namespace GerenciamentoComercio_Domain.v1.Repositories
{
    public class ProductHistoricRepository : Repository<ProductHistoric>, IProductHistoricRepository
    {
        public ProductHistoricRepository(GerenciamentoComercioContext context) : base(context)
        {
        }

        public List<ProductHistoric> GetHistoricByProductId(int productId)
        {
            return _context.ProductHistoric
                .Where(x => x.IdProduct == productId)
                .ToList();
        }
    }
}
