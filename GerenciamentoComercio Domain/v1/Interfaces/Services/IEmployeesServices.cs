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
        APIMessage UpdateEmployee(UpdateEmployeeRequest request, int id, int userCode);
        Task<APIMessage> DeleteEmployeeAsync(int id);
    }
}
