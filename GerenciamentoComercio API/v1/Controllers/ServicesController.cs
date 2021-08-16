using GerenciamentoComercio_Domain.DTOs.Services;
using GerenciamentoComercio_Domain.Utils.APIMessage;
using GerenciamentoComercio_Domain.Utils.IUserApp;
using GerenciamentoComercio_Domain.Utils.ModelStateValidation;
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
    [Route("v{version:apiVersion}/services/")]
    [ApiVersion("1.0")]
    public class ServicesController : MainController
    {
        private readonly IServicesServices _servicesServices;
        public ServicesController(IUserApp userApp,
             INotifier notifier,
             IServicesServices servicesServices) : base(userApp, notifier)
        {
            _servicesServices = servicesServices;
        }

        [HttpGet]
        [SwaggerOperation("Returns all services")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(List<GetAllServicesResponse>))]
        public IActionResult GetAllServiceAsync()
        {
            APIMessage response = _servicesServices.GetAllServices();

            return StatusCode((int)response.StatusCode, response.ContentObj);
        }

        [HttpGet("{id}")]
        [SwaggerOperation("Returns a service by id")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(GetServiceByIdResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Service not found", typeof(string))]
        public IActionResult GetServiceById(int id)
        {
            APIMessage response = _servicesServices.GetServiceById(id);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return StatusCode((int)response.StatusCode, response.Content);
            }

            return StatusCode((int)response.StatusCode, response.ContentObj);
        }

        [HttpGet("by-category/{categoryId}")]
        [SwaggerOperation("Returns a service by category")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(GetServiceByIdResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Category not found", typeof(string))]
        public IActionResult GetServiceByCategory(int categoryId)
        {
            APIMessage response = _servicesServices.GetServicesByCategory(categoryId);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return StatusCode((int)response.StatusCode, response.Content);
            }

            return StatusCode((int)response.StatusCode, response.ContentObj);
        }

        [HttpPost]
        [SwaggerOperation("Add a new service")]
        [SwaggerResponse(StatusCodes.Status200OK, "Service created successfully", typeof(string))]
        public async Task<IActionResult> AddNewServiceAsync(AddNewServiceRequest request)
        {
            if (!ModelState.IsValid) return CustomReturn(ModelState);

            APIMessage response = await _servicesServices.AddNewServiceAsync(request, UserName);

            return StatusCode((int)response.StatusCode, response.Content);
        }

        [HttpPut("{id}")]
        [SwaggerOperation("Updates a service")]
        [SwaggerResponse(StatusCodes.Status200OK, "Service updated successfully", typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Service not found", typeof(string))]
        public async Task<IActionResult> UpdateServiceAsync(UpdateServiceRequest request, int id)
        {
            APIMessage response = await _servicesServices.UpdateServiceAsync(request, id, UserName);

            return StatusCode((int)response.StatusCode, response.Content);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation("Deletes a service")]
        [SwaggerResponse(StatusCodes.Status200OK, "Service deleted successfully", typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Service not found", typeof(string))]
        public IActionResult DeleteServiceAsync(int id)
        {
            APIMessage response = _servicesServices.DeleteService(id);

            return StatusCode((int)response.StatusCode, response.Content);
        }
    }
}
