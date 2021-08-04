using GerenciamentoComercio_Domain.DTOs.Employees;
using GerenciamentoComercio_Domain.Utils.APIMessage;
using GerenciamentoComercio_Domain.v1.Interfaces.Repositories;
using GerenciamentoComercio_Domain.v1.Interfaces.Services;
using GerenciamentoComercio_Infra.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GerenciamentoComercio_Domain.v1.Services
{
    public class EmployeesServices : IEmployeesServices
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesServices(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<APIMessage> GetAllEmployessAsync()
        {
            IEnumerable<Employee> employees = await _employeeRepository.GetMany();

            return new APIMessage(HttpStatusCode
                .OK, employees.Select(x => new GetAllEmployeesResponse
                {
                    Address = x.Address,
                    Email = x.Email,
                    FullName = x.FullName,
                    Id = x.Id,
                    IsAdministrator = x.IsAdministrator,
                    Phone = x.Phone
                }));
        }
    }
}
