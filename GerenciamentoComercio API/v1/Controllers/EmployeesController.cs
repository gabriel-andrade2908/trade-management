using GerenciamentoComercio_Domain.DTOs.Employees;
using GerenciamentoComercio_Domain.Utils.APIMessage;
using GerenciamentoComercio_Domain.Utils.IUserApp;
using GerenciamentoComercio_Domain.v1.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace GerenciamentoComercio_API.v1.Controllers
{
    [Authorize]
    [Route("v{version:apiVersion}/employees/")]
    [ApiVersion("1.0")]
    public class EmployeesController : MainController
    {
        private readonly IEmployeesServices _employeesServices;

        public EmployeesController(IEmployeesServices employeesServices,
            IUserApp userApp) : base(userApp)
        {
            _employeesServices = employeesServices;
        }

        [HttpGet]
        [SwaggerOperation("Returns all employees")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(List<GetEmployeesResponse>))]
        public async Task<IActionResult> GetAllEmployessAsync()
        {
            APIMessage response = await _employeesServices.GetAllEmployessAsync();

            return StatusCode((int)response.StatusCode, response.ContentObj);
        }

        [HttpGet("{id}")]
        [SwaggerOperation("Returns an employee by id")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(List<GetEmployeesResponse>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "User not found", typeof(string))]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            APIMessage response = await _employeesServices.GetEmployeeById(id);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return StatusCode((int)response.StatusCode, response.Content);
            }

            return StatusCode((int)response.StatusCode, response.ContentObj);
        }

        [HttpPost]
        [SwaggerOperation("Add a new employee")]
        [SwaggerResponse(StatusCodes.Status200OK, "User created successfully", typeof(string))]
        public IActionResult AddNewEmployee(AddNewEmployeeRequest request)
        {
            APIMessage response =  _employeesServices.AddNewEmployee(request, UserName);

            return StatusCode((int)response.StatusCode, response.Content);
        }

        [HttpPut("{id}")]
        [SwaggerOperation("Updates an employee")]
        [SwaggerResponse(StatusCodes.Status200OK, "User updated successfully", typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "User not found", typeof(string))]
        public async Task<IActionResult> UpdateEmployeeAsync(UpdateEmployeeRequest request, int id)
        {
            APIMessage response = await _employeesServices.UpdateEmployeeAsync(request, id);

            return StatusCode((int)response.StatusCode, response.Content);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation("Deletes an employee")]
        [SwaggerResponse(StatusCodes.Status200OK, "User deleted successfully", typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "User not found", typeof(string))]
        public async Task<IActionResult> DeleteEmployeeAsync(int id)
        {
            APIMessage response = await _employeesServices.DeleteEmployeeAsync(id);

            return StatusCode((int)response.StatusCode, response.Content);
        }
    }
}
