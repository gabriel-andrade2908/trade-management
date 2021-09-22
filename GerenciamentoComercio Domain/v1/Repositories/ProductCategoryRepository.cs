using GerenciamentoComercio_Domain.Utils.IRepository;
using GerenciamentoComercio_Domain.v1.Interfaces.Repositories;
using GerenciamentoComercio_Infra.Context;
using GerenciamentoComercio_Infra.Models;
using System.Linq;

namespace GerenciamentoComercio_Domain.v1.Repositories
{
    public class ProductCategoryRepository : Repository<ProductCategory>, IProductCategoryRepository 
    {
        public ProductCategoryRepository(GerenciamentoComercioContext context) : base(context)
        {
        }

        public ProductCategory GetCategoryByTitle(string title)
        {
            return _context.ProductCategory.FirstOrDefault(x => x.Title == title);
        }
    }
}
