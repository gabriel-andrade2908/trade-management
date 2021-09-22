using GerenciamentoComercio_Domain.Utils.IRepository;
using GerenciamentoComercio_Domain.v1.Interfaces.Repositories;
using GerenciamentoComercio_Infra.Context;
using GerenciamentoComercio_Infra.Models;
using System.Collections.Generic;
using System.Linq;

namespace GerenciamentoComercio_Domain.v1.Repositories
{
    public class ServiceHistoricRepository : Repository<ServiceHistoric>, IServiceHistoricRepository
    {
        public ServiceHistoricRepository(GerenciamentoComercioContext context) : base(context)
        {
        }

        public List<ServiceHistoric> GetHistoricByServiceId(int serviceId)
        {
            return _context.ServiceHistoric
                .Where(x => x.IdService == serviceId)
                .ToList();
        }
    }
}
