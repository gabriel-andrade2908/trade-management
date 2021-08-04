using GerenciamentoComercio_Domain.Utils.APIMessage;
using System.Threading.Tasks;

namespace GerenciamentoComercio_Domain.v1.Interfaces.Services
{
    public interface IEmployeesServices
    {
        Task<APIMessage> GetAllEmployessAsync();
    }
}
