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
    [Route("v{version:apiVersion}/product-transaction/")]
    [ApiVersion("1.0")]
    public class ProductTransactionController : MainController
    {
        private readonly IProductTransactionServices _productTransactionServices;
        public ProductTransactionController(IUserApp userApp,
             INotifier notifier,
             IProductTransactionServices productTransactionServices) : base(userApp, notifier)
        {
            _productTransactionServices = productTransactionServices;
        }

        [HttpPost("{clientTransactionId}")]
        [SwaggerOperation("Add a new product transaction to an existing client transaction")]
        [SwaggerResponse(StatusCodes.Status200OK, "Product transaction added successfully", typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product not found", typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Client transaction not found", typeof(string))]
        public async Task<IActionResult> AddNewProductTransactionAsync(List<AddNewProductServiceTransactionRequest> products, int clientTransactionId)
        {
            if (!ModelState.IsValid) return CustomReturn(ModelState);

            APIMessage response = await _productTransactionServices
                .AddNewProductTransactionAsync(products, clientTransactionId, UserName);

            return StatusCode((int)response.StatusCode, response.Content);
        }

        [HttpPut("{id}")]
        [SwaggerOperation("Updates a product transaction and client transaction")]
        [SwaggerResponse(StatusCodes.Status200OK, "Product transaction updated successfully", typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product transaction not found", typeof(string))]
        public async Task<IActionResult> UpdateProductTransactionAsync(int quantity, int id)
        {
            APIMessage response = await _productTransactionServices
                .UpdateProductTransactionAsync(quantity, id);

            return StatusCode((int)response.StatusCode, response.Content);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation("Deletes a product transaction and updates client transaction")]
        [SwaggerResponse(StatusCodes.Status200OK, "Product transaction deleted successfully", typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product transaction not found", typeof(string))]
        public async Task<IActionResult> DeleteProductTransactionAsync(int id)
        {
            APIMessage response = await _productTransactionServices
                .DeleteProductTransactionAsync(id);

            return StatusCode((int)response.StatusCode, response.Content);
        }
    }
}
