using GerenciamentoComercio_Domain.DTOs.Auth;
using GerenciamentoComercio_Domain.Utils.IRepository;
using GerenciamentoComercio_Domain.Utils.Security;
using GerenciamentoComercio_Domain.v1.Interfaces.Repositories;
using GerenciamentoComercio_Infra.Context;
using GerenciamentoComercio_Infra.Models;
using System.Linq;

namespace GerenciamentoComercio_Domain.v1.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(GerenciamentoComercioContext context) : base(context)
        {

        }

        public Employee CheckLogin(LoginRequest request)
        {
            return _context.Employees
                .Where(x => x.Password == Security.EncryptString(request.Password) && 
                x.Access == request.Access).FirstOrDefault();
        }
    }
}
