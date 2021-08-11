using GerenciamentoComercio_Domain.DTOs.Employees;
using GerenciamentoComercio_Domain.Utils.APIMessage;
using GerenciamentoComercio_Domain.Utils.PasswordGenerator;
using GerenciamentoComercio_Domain.Utils.Security;
using GerenciamentoComercio_Domain.Utils.UnitOfWork;
using GerenciamentoComercio_Domain.v1.Interfaces.Repositories;
using GerenciamentoComercio_Domain.v1.Interfaces.Services;
using GerenciamentoComercio_Infra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GerenciamentoComercio_Domain.v1.Services
{
    public class EmployeesServices : IEmployeesServices
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeesServices(IEmployeeRepository employeeRepository,
            IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<APIMessage> GetAllEmployeesAsync()
        {
            IEnumerable<Employee> employees = await _employeeRepository.GetMany();

            return new APIMessage(HttpStatusCode.OK, employees
                .Select(x => new GetEmployeesResponse
                {
                    Address = x.Address,
                    Email = x.Email,
                    FullName = x.FullName,
                    Id = x.Id,
                    IsAdministrator = x.IsAdministrator,
                    Phone = x.Phone,
                    Access = x.Access
                }));
        }

        public async Task<APIMessage> GetEmployeeById(int id)
        {
            Employee employee = await _employeeRepository.GetById(id);

            if (employee == null)
            {
                return new APIMessage(HttpStatusCode.NotFound,
                    new List<string> { "Usuário não encontrado." });
            }

            return new APIMessage(HttpStatusCode.OK, new GetEmployeeByIdResponse
            {
                Address = employee.Address,
                Email = employee.Email,
                FullName = employee.FullName,
                IsAdministrator = employee.IsAdministrator,
                Phone = employee.Phone,
                Access = employee.Access
            });
        }

        public APIMessage AddNewEmployee(AddNewEmployeeRequest request, string userName)
        {
            APIMessage validation = ValidadeAddNewEmployee(request);

            if (validation.StatusCode != HttpStatusCode.OK)
            {
                return new APIMessage(validation.StatusCode, validation.Content);
            }

            var newEmployee = new Employee
            {
                Access = request.Access,
                Address = request.Address,
                CreationDate = DateTime.Now,
                CreationUser = userName,
                Email = request.Email,
                FullName = request.FullName,
                IsAdministrator = request.IsAdministrator,
                Password = request.GeneratePassword ? Security.EncryptString(PasswordGenerator.GerarSenha(8)) :
                Security.EncryptString(request.Password),
                Phone = request.Phone,
            };

            _employeeRepository.AddNew(newEmployee);

            _unitOfWork.Commit();

            return new APIMessage(HttpStatusCode.OK, new List<string> { "Usuário cadastrado com sucesso." });
        }

        public APIMessage UpdateEmployee(UpdateEmployeeRequest request, int id, int userCode)
        {
            var validation = ValidadeUpdateEmployee(request, id, userCode);

            if (validation.StatusCode != HttpStatusCode.OK)
            {
                return new APIMessage(validation.StatusCode, validation.Content);
            }

            Employee employee = (Employee)validation.ContentObj;

            employee.Access = request.Access ?? employee.Access;
            employee.Address = request.Address ?? employee.Address;
            employee.Email = request.Email ?? employee.Email;
            employee.FullName = request.FullName ?? employee.FullName;
            employee.IsAdministrator = request.IsAdministrator ?? employee.IsAdministrator;
            employee.Phone = request.Phone ?? employee.Phone;
            employee.Password = Security.EncryptString(request.Password) ?? employee.Password;

            _employeeRepository.Update(employee);

            _unitOfWork.Commit();

            return new APIMessage(HttpStatusCode.OK, new List<string> { "Usuário atualizado com sucesso." });
        }

        public async Task<APIMessage> DeleteEmployeeAsync(int id)
        {
            Employee employee = await _employeeRepository.GetById(id);

            if (employee == null)
            {
                return new APIMessage(HttpStatusCode.NotFound,
                    new List<string> { "Usuário não encontrado." });
            }

            _employeeRepository.Delete(employee);

            _unitOfWork.Commit();

            return new APIMessage(HttpStatusCode.OK, new List<string> { "Usuário excluído com sucesso." });
        }

        private APIMessage ValidadeAddNewEmployee(AddNewEmployeeRequest request)
        {
            if (!PasswordGenerator.ValidatePassword(request.Password, 8, 1, 0, 1, 1, 1) &&
                !string.IsNullOrEmpty(request.Password))
                return new APIMessage(HttpStatusCode
                    .BadRequest, new List<string> {"A senha deve conter 1 letra maiúscula, 1 letra minúscula," +
                    " 1 caractere especial, 1 número e no mínimo 8 caracteres." });

            if (request.GeneratePassword && !string.IsNullOrEmpty(request.Password) ||
                !request.GeneratePassword && string.IsNullOrEmpty(request.Password))
            {
                return new APIMessage(HttpStatusCode
                    .BadRequest, new List<string> { "Favor inserir uma senha ou gerar uma senha automática." });
            }

            Employee checkIfExistUserEmail = _employeeRepository.GetUserByEmail(request.Email);

            if (checkIfExistUserEmail != null)
            {
                return new APIMessage(HttpStatusCode
                    .BadRequest, new List<string> { "Já existe um usuário cadastrado com este e-mail." });
            }

            return new APIMessage(HttpStatusCode.OK, "");
        }

        private APIMessage ValidadeUpdateEmployee(UpdateEmployeeRequest request, int id, int userCode)
        {
            Task<Employee> employee = _employeeRepository.GetById(id);

            Task<Employee> loggedUser = _employeeRepository.GetById(userCode);

            if (employee == null)
            {
                return new APIMessage(HttpStatusCode.NotFound,
                    new List<string> { "Usuário não encontrado." });
            }

            Employee checkIfExistUserEmail = _employeeRepository.GetUserByEmail(request.Email);

            if (checkIfExistUserEmail != null)
            {
                return new APIMessage(HttpStatusCode
                    .BadRequest, new List<string> { "Já existe um usuário cadastrado com este e-mail." });
            }

            if (!string.IsNullOrEmpty(request.Password) && !loggedUser.Result.IsAdministrator.Value)
            {
                return new APIMessage(HttpStatusCode
                    .BadRequest, new List<string> { "Usuario não tem permissão para alteração de senha." });
            }

            return new APIMessage(HttpStatusCode.OK, employee);
        }
    }
}
