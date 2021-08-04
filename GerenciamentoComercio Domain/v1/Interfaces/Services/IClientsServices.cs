using GerenciamentoComercio_Domain.DTOs.Clients;
using GerenciamentoComercio_Domain.Utils.APIMessage;
using System.Threading.Tasks;

namespace GerenciamentoComercio_Domain.v1.Interfaces.Services
{
    public interface IClientsServices
    {
        Task<APIMessage> GetAllClientsAsync();
        Task<APIMessage> GetClientById(int id);
        APIMessage AddNewClient(AddNewClientRequest request, string userName);
        Task<APIMessage> UpdateClientAsync(AddNewClientRequest request, int id);
        Task<APIMessage> DeleteClientAsync(int id);
    }
}
