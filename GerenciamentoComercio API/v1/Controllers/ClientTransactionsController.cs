using GerenciamentoComercio_Domain.DTOs;
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
using System.Net;
using System.Threading.Tasks;

namespace GerenciamentoComercio_API.v1.Controllers
{
    [Authorize]
    [Route("v{version:apiVersion}/client-transactions/")]
    [ApiVersion("1.0")]
    public class ClientTransactionsController : MainController
    {
        private readonly IClientTransactionsServices _clientTransactionsServices;
        public ClientTransactionsController(IUserApp userApp,
            INotifier notifier,
            IClientTransactionsServices clientTransactionsServices) : base(userApp, notifier)
        {
            _clientTransactionsServices = clientTransactionsServices;
        }

        [HttpGet("by-product/{productCode}")]
        [SwaggerOperation("Returns Transacations by product")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(List<GetProductTransactionsResponse>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product not found", typeof(string))]
        public IActionResult GetTransacationsByProduct(int productCode)
        {
            APIMessage response =  _clientTransactionsServices
                .GetTransactionsByProduct(productCode);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return StatusCode((int)response.StatusCode, response.Content);
            }

            return StatusCode((int)response.StatusCode, response.ContentObj);
        }

        [HttpGet("by-service/{serviceCode}")]
        [SwaggerOperation("Returns Transacations by service")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(List<GetProductTransactionsResponse>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Service not found", typeof(string))]
        public IActionResult GetTransacationsByServices(int serviceCode)
        {
            APIMessage response = _clientTransactionsServices
                .GetTransactionsByService(serviceCode);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return StatusCode((int)response.StatusCode, response.Content);
            }

            return StatusCode((int)response.StatusCode, response.ContentObj);
        }

        [HttpGet("by-client/{clientCode}")]
        [SwaggerOperation("Returns Transacations by client")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(List<GetProductTransactionsResponse>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Client not found", typeof(string))]
        public IActionResult GetTransacationsByClient(int clientCode)
        {
            APIMessage response = _clientTransactionsServices
                .GetTransactionsByClient(clientCode);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return StatusCode((int)response.StatusCode, response.Content);
            }

            return StatusCode((int)response.StatusCode, response.ContentObj);
        }

        [HttpGet("by-service/{employeeCode}")]
        [SwaggerOperation("Returns Transacations by employee")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(List<GetProductTransactionsResponse>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Employee not found", typeof(string))]
        public IActionResult GetTransacationsByEmployee(int employeeCode)
        {
            APIMessage response = _clientTransactionsServices
                .GetTransacationsByEmployee(employeeCode);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return StatusCode((int)response.StatusCode, response.Content);
            }

            return StatusCode((int)response.StatusCode, response.ContentObj);
        }

        [HttpPost]
        [SwaggerOperation("Add a new transacation")]
        [SwaggerResponse(StatusCodes.Status200OK, "Transaction created successfully", typeof(string))]
        public IActionResult AddNewTransactionAsync(AddNewClientTransactionRequest request)
        {
            if (!ModelState.IsValid) return CustomReturn(ModelState);

            APIMessage response = _clientTransactionsServices
                .AddNewClientTransactionAsync(request, UserName);

            return StatusCode((int)response.StatusCode, response.Content);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation("Deletes a client transaction")]
        [SwaggerResponse(StatusCodes.Status200OK, "Transaction deleted successfully", typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Transaction not found", typeof(string))]
        public async Task<IActionResult> DeleteTransactionAsync(int id)
        {
            APIMessage response = await _clientTransactionsServices.DeleteClientTransactionAsync(id);

            return StatusCode((int)response.StatusCode, response.Content);
        }
    }
}
