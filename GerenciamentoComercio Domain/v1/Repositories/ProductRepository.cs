using GerenciamentoComercio_Domain.Utils.IRepository;
using GerenciamentoComercio_Domain.v1.Interfaces.Repositories;
using GerenciamentoComercio_Infra.Context;
using GerenciamentoComercio_Infra.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GerenciamentoComercio_Domain.v1.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository 
    {
        public ProductRepository(GerenciamentoComercioContext context) : base(context)
        {
        }

        public new IEnumerable<Product> GetMany()
        {
            return _context.Product.Include(x => x.IdProductCategoryNavigation);
        }

        public new Product GetById(int id)
        {
            return _context.Product.Include(x => x.IdProductCategoryNavigation)
                .FirstOrDefault(x => x.Id == id);
        }

        public List<Product> GetProductByCategory(int categoryId)
        {
            return _context.Product.Include(x => x.IdProductCategoryNavigation)
                .Where(x => x.IdProductCategory == categoryId).ToList();
        }
    }
}
