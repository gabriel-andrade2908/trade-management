using GerenciamentoComercio_Domain.Utils.IRepository;
using GerenciamentoComercio_Domain.v1.Interfaces.Repositories;
using GerenciamentoComercio_Infra.Context;
using GerenciamentoComercio_Infra.Models;
using System.Linq;

namespace GerenciamentoComercio_Domain.v1.Repositories
{
    public class ServiceCategoryRepository : Repository<ServiceCategory>, IServiceCategoryRepository 
    {
        public ServiceCategoryRepository(GerenciamentoComercioContext context) : base(context)
        {
        }

        public ServiceCategory GetCategoryByTitle(string title)
        {
            return _context.ServiceCategories.FirstOrDefault(x => x.Title == title);
        }
    }
}
