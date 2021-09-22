using GerenciamentoComercio_Domain.DTOs.ProductTransaction;
using GerenciamentoComercio_Domain.Utils.APIMessage;
using GerenciamentoComercio_Domain.Utils.IUserApp;
using GerenciamentoComercio_Domain.Utils.ModelStateValidation;
using GerenciamentoComercio_Domain.v1.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerenciamentoComercio_API.v1.Controllers
{
    [Authorize]
    [Route("v{version:apiVersion}/service-transaction/")]
    [ApiVersion("1.0")]
    public class ServiceTransactionController : MainController
    {
        private readonly IServiceTransactionServices _serviceTransactionServices;
        public ServiceTransactionController(IUserApp userApp,
             INotifier notifier,
             IServiceTransactionServices serviceTransactionServices) : base(userApp, notifier)
        {
            _serviceTransactionServices = serviceTransactionServices;
        }

        [HttpPost("{clientTransactionId}")]
        [SwaggerOperation("Add a new service transaction to an existing client transaction")]
        [SwaggerResponse(StatusCodes.Status200OK, "Service transaction added successfully", typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Service not found", typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Client transaction not found", typeof(string))]
        public async Task<IActionResult> AddNewServiceTransactionAsync(List<AddNewProductServiceTransactionRequest> services, int clientTransactionId)
        {
            if (!ModelState.IsValid) return CustomReturn(ModelState);

            APIMessage response = await _serviceTransactionServices
                .AddNewServiceTransactionAsync(services, clientTransactionId, UserName);

            return StatusCode((int)response.StatusCode, response.Content);
        }

        [HttpPut("{id}")]
        [SwaggerOperation("Updates a service transaction and client transaction")]
        [SwaggerResponse(StatusCodes.Status200OK, "Service transaction updated successfully", typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Service transaction not found", typeof(string))]
        public async Task<IActionResult> UpdateServiceTransactionAsync(int quantity, int id)
        {
            APIMessage response = await _serviceTransactionServices
                .UpdateServiceTransactionAsync(quantity, id);

            return StatusCode((int)response.StatusCode, response.Content);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation("Deletes a service transaction and updates client transaction")]
        [SwaggerResponse(StatusCodes.Status200OK, "Service transaction deleted successfully", typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Service transaction not found", typeof(string))]
        public async Task<IActionResult> DeleteServiceTransactionAsync(int id)
        {
            APIMessage response = await _serviceTransactionServices
                .DeleteServiceTransactionAsync(id);

            return StatusCode((int)response.StatusCode, response.Content);
        }
    }
}
