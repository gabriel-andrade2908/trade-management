using GerenciamentoComercio_Domain.DTOs.ServiceHistoric;
using GerenciamentoComercio_Domain.Utils.APIMessage;
using GerenciamentoComercio_Domain.Utils.IUserApp;
using GerenciamentoComercio_Domain.Utils.ModelStateValidation;
using GerenciamentoComercio_Domain.v1.Interfaces.Repositories;
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
    [Route("v{version:apiVersion}/services-historic/")]
    [ApiVersion("1.0")]
    public class ServicesHistoricController : MainController
    {
        private readonly IServicesHistoricServices _servicesHistoricServices;
        public ServicesHistoricController(IUserApp userApp,
             INotifier notifier,
             IServicesHistoricServices servicesHistoricServices) : base(userApp, notifier)
        {
            _servicesHistoricServices = servicesHistoricServices;
        }

        [HttpGet]
        [SwaggerOperation("Returns all services historic")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(List<GetAllServicesHistoricResponse>))]
        public async Task<IActionResult> GetAllServicesHistoricAsync()
        {
            APIMessage response = await _servicesHistoricServices.GetAllServicesHistoricAsync();

            return StatusCode((int)response.StatusCode, response.ContentObj);
        }

        [HttpGet("by-id/{id}")]
        [SwaggerOperation("Returns a service historic by id")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(GetServiceHistoricByIdResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Service historic not found", typeof(string))]
        public async Task<IActionResult> GetServiceHistoricByIdAsync(int id)
        {
            APIMessage response = await _servicesHistoricServices.GetServiceHistoricByIdAsync(id);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return StatusCode((int)response.StatusCode, response.Content);
            }

            return StatusCode((int)response.StatusCode, response.ContentObj);
        }

        [HttpGet("by-service/{serviceId}")]
        [SwaggerOperation("Returns a historic by service")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(GetServicesHistoricByServiceResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Service historic not found", typeof(string))]
        public IActionResult GetHistoricByServiceAsync(int serviceId)
        {
            APIMessage response = _servicesHistoricServices.GetHistoricByServiceAsync(serviceId);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return StatusCode((int)response.StatusCode, response.Content);
            }

            return StatusCode((int)response.StatusCode, response.ContentObj);
        }
    }
}
