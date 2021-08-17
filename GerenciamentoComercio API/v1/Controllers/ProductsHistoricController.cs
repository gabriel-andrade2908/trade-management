using GerenciamentoComercio_Domain.DTOs.ProductHistoric;
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
    [Route("v{version:apiVersion}/products-historic/")]
    [ApiVersion("1.0")]
    public class ProductsHistoricController : MainController
    {
        private readonly IProductsHistoricServices _productsHistoricServices;
        public ProductsHistoricController(IUserApp userApp,
             INotifier notifier,
             IProductsHistoricServices productsHistoricServices) : base(userApp, notifier)
        {
            _productsHistoricServices = productsHistoricServices;
        }

        [HttpGet]
        [SwaggerOperation("Returns all products historic")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(List<GetAllProductsHistoricResponse>))]
        public async Task<IActionResult> GetAllProductsHistoricAsync()
        {
            APIMessage response = await _productsHistoricServices.GetAllProductsHistoricAsync();

            return StatusCode((int)response.StatusCode, response.ContentObj);
        }

        [HttpGet("by-id/{id}")]
        [SwaggerOperation("Returns a product historic by id")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(GetProductHistoricByIdResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product historic not found", typeof(string))]
        public async Task<IActionResult> GetProductHistoricByIdAsync(int id)
        {
            APIMessage response = await _productsHistoricServices.GetProductHistoricByIdAsync(id);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return StatusCode((int)response.StatusCode, response.Content);
            }

            return StatusCode((int)response.StatusCode, response.ContentObj);
        }

        [HttpGet("by-product/{productId}")]
        [SwaggerOperation("Returns a historic by product")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(GetProductHistoricByProductResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product historic not found", typeof(string))]
        public IActionResult GetHistoricByProductAsync(int productId)
        {
            APIMessage response =  _productsHistoricServices.GetHistoricByProductAsync(productId);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return StatusCode((int)response.StatusCode, response.Content);
            }

            return StatusCode((int)response.StatusCode, response.ContentObj);
        }

        //[HttpPost]
        //[SwaggerOperation("Add a new product historic")]
        //[SwaggerResponse(StatusCodes.Status200OK, "Historic created successfully", typeof(string))]
        //public IActionResult AddNewProductHistoricAsync(AddNewProductHistoricRequest request)
        //{
        //    if (!ModelState.IsValid) return CustomReturn(ModelState);

        //    APIMessage response = _productsHistoricServices.AddNewProductHistoricAsync(request, UserName);

        //    return StatusCode((int)response.StatusCode, response.Content);
        //}

        //[HttpPut("{id}")]
        //[SwaggerOperation("Updates a product historic")]
        //[SwaggerResponse(StatusCodes.Status200OK, "Historic updated successfully", typeof(string))]
        //[SwaggerResponse(StatusCodes.Status404NotFound, "Product historic not found", typeof(string))]
        //public async Task<IActionResult> UpdateProductHistoricAsync(UpdateProductHistoricRequest request, int id)
        //{
        //    APIMessage response = await _productsHistoricServices.UpdateProductHistoricAsync(request, id);

        //    return StatusCode((int)response.StatusCode, response.Content);
        //}

        //[HttpDelete("{id}")]
        //[SwaggerOperation("Deletes a product historic")]
        //[SwaggerResponse(StatusCodes.Status200OK, "Historic deleted successfully", typeof(string))]
        //[SwaggerResponse(StatusCodes.Status404NotFound, "Product historic not found", typeof(string))]
        //public async Task<IActionResult> DeleteProductHistoricAsync(int id)
        //{
        //    APIMessage response = await _productsHistoricServices.DeleteProductHistoricAsync(id);

        //    return StatusCode((int)response.StatusCode, response.Content);
        //}
    }
}
