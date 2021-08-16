using GerenciamentoComercio_Domain.DTOs.Services;
using GerenciamentoComercio_Domain.Utils.APIMessage;
using System.Threading.Tasks;

namespace GerenciamentoComercio_Domain.v1.Interfaces.Services
{
    public interface IServicesServices
    {
        APIMessage GetAllServices();
        APIMessage GetServiceById(int id);
        Task<APIMessage> AddNewServiceAsync(AddNewServiceRequest request, string userName);
        Task<APIMessage> UpdateServiceAsync(UpdateServiceRequest request, int id, string userName);
        APIMessage DeleteService(int id);
        APIMessage GetServicesByCategory(int categoryId);
    }
}
