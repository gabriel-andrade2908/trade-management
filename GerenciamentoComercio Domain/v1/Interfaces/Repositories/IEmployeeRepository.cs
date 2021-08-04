using GerenciamentoComercio_Domain.DTOs.Auth;
using GerenciamentoComercio_Domain.Utils.IRepository;
using GerenciamentoComercio_Infra.Models;

namespace GerenciamentoComercio_Domain.v1.Interfaces.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Employee CheckLogin(LoginRequest request);
    }
}
