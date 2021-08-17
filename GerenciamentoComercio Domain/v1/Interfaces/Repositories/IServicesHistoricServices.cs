using GerenciamentoComercio_Domain.Utils.APIMessage;
using System.Threading.Tasks;

namespace GerenciamentoComercio_Domain.v1.Interfaces.Repositories
{
    public interface IServicesHistoricServices
    {
        Task<APIMessage> GetAllServicesHistoricAsync();
        Task<APIMessage> GetServiceHistoricByIdAsync(int id);
        APIMessage GetHistoricByServiceAsync(int serviceId);
    }
}
