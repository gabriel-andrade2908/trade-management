using GerenciamentoComercio_Domain.DTOs.Employees;
using GerenciamentoComercio_Domain.Utils.APIMessage;
using System.Threading.Tasks;

namespace GerenciamentoComercio_Domain.v1.Interfaces.Services
{
    public interface IEmployeesServices
    {
        Task<APIMessage> GetAllEmployeesAsync();
        Task<APIMessage> GetEmployeeById(int id);
        APIMessage AddNewEmployee(AddNewEmployeeRequest request, string userName);
        Task<APIMessage> UpdateEmployeeAsync(UpdateEmployeeRequest request, int id);
        Task<APIMessage> DeleteEmployeeAsync(int id);
    }
}
