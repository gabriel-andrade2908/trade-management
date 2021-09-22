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
    public class ServiceRepository : Repository<Service>, IServiceRepository 
    {
        public ServiceRepository(GerenciamentoComercioContext context) : base(context)
        {
        }

        public new IEnumerable<Service> GetMany()
        {
            return _context.Service.Include(x => x.IdServiceCategoryNavigation);
        }

        public new Service GetById(int id)
        {
            return _context.Service.Include(x => x.IdServiceCategoryNavigation)
                .FirstOrDefault(x => x.Id == id);
        }

        public List<Service> GetServiceByCategory(int categoryId)
        {
            return _context.Service.Include(x => x.IdServiceCategoryNavigation)
                .Where(x => x.IdServiceCategory == categoryId).ToList();
        }
    }
}
