using GerenciamentoComercio_Domain.Utils.IRepository;
using GerenciamentoComercio_Infra.Models;
using System.Collections.Generic;

namespace GerenciamentoComercio_Domain.v1.Interfaces.Repositories
{
    public interface IServiceHistoricRepository : IRepository<ServiceHistoric>
    {
        List<ServiceHistoric> GetHistoricByServiceId(int serviceId);
    }
}
