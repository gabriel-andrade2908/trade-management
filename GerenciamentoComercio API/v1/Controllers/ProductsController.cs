using GerenciamentoComercio_Domain.DTOs.Products;
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
    [Route("v{version:apiVersion}/products/")]
    [ApiVersion("1.0")]
    public class ProductsController : MainController
    {
        private readonly IProductsServices _productsServices;
        public ProductsController(IUserApp userApp,
             INotifier notifier,
             IProductsServices productsServices) : base(userApp, notifier)
        {
            _productsServices = productsServices;
        }

        [HttpGet]
        [SwaggerOperation("Returns all products")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(List<GetAllProductsResponse>))]
        public IActionResult GetAllProductsAsync()
        {
            APIMessage response =  _productsServices.GetAllProducts();

            return StatusCode((int)response.StatusCode, response.ContentObj);
        }

        [HttpGet("{id}")]
        [SwaggerOperation("Returns a product by id")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(GetProductByIdResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product not found", typeof(string))]
        public IActionResult GetProductById(int id)
        {
            APIMessage response = _productsServices.GetProductById(id);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return StatusCode((int)response.StatusCode, response.Content);
            }

            return StatusCode((int)response.StatusCode, response.ContentObj);
        }

        [HttpGet("by-category/{categoryId}")]
        [SwaggerOperation("Returns a product by category")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(GetProductByIdResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Category not found", typeof(string))]
        public IActionResult GetProductByCategory(int categoryId)
        {
            APIMessage response = _productsServices.GetProductByCategory(categoryId);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return StatusCode((int)response.StatusCode, response.Content);
            }

            return StatusCode((int)response.StatusCode, response.ContentObj);
        }

        [HttpPost]
        [SwaggerOperation("Add a new product")]
        [SwaggerResponse(StatusCodes.Status200OK, "Product created successfully", typeof(string))]
        public async Task<IActionResult> AddNewProductAsync(AddNewProductRequest request)
        {
            if (!ModelState.IsValid) return CustomReturn(ModelState);

            APIMessage response = await _productsServices.AddNewProductAsync(request, UserName);

            return StatusCode((int)response.StatusCode, response.Content);
        }

        [HttpPut("{id}")]
        [SwaggerOperation("Updates a product")]
        [SwaggerResponse(StatusCodes.Status200OK, "Product updated successfully", typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product not found", typeof(string))]
        public async Task<IActionResult> UpdateProductAsync(UpdateProductRequest request, int id)
        {
            APIMessage response = await _productsServices.UpdateProductAsync(request, id);

            return StatusCode((int)response.StatusCode, response.Content);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation("Deletes a product")]
        [SwaggerResponse(StatusCodes.Status200OK, "Product deleted successfully", typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product not found", typeof(string))]
        public IActionResult DeleteProductAsync(int id)
        {
            APIMessage response = _productsServices.DeleteProduct(id);

            return StatusCode((int)response.StatusCode, response.Content);
        }
    }
}
