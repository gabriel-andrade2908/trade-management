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
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(List<GetAllEmployeesResponse>))]
        public async Task<IActionResult> GetAllEmployessAsync()
        {
            APIMessage response = await _employeesServices.GetAllEmployessAsync();

            return StatusCode((int)response.StatusCode, response.ContentObj);
        }
    }
}
