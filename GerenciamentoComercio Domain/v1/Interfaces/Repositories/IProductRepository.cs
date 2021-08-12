using GerenciamentoComercio_Domain.Utils.IRepository;
using GerenciamentoComercio_Infra.Models;
using System.Collections.Generic;

namespace GerenciamentoComercio_Domain.v1.Interfaces.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        new Product GetById(int id);
        new IEnumerable<Product> GetMany();
    }
}
