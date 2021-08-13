using GerenciamentoComercio_Domain.DTOs.ProductCategories;
using GerenciamentoComercio_Domain.Utils.APIMessage;
using GerenciamentoComercio_Domain.Utils.IUserApp;
using GerenciamentoComercio_Domain.Utils.ModelStateValidation;
using GerenciamentoComercio_Domain.v1.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GerenciamentoComercio_API.v1.Controllers
{
    [Authorize]
    [Route("v{version:apiVersion}/product-categories/")]
    [ApiVersion("1.0")]
    public class ProductCategoriesController : MainController
    {
        private readonly IProductCategoriesServices _productCategoriesServices;
        public ProductCategoriesController(IUserApp userApp,
             INotifier notifier,
             IProductCategoriesServices productCategoriesServices) : base(userApp, notifier)
        {
            _productCategoriesServices = productCategoriesServices;
        }

        [HttpGet]
        [SwaggerOperation("Returns all product categories")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(List<GetAllProductCategoriesResponse>))]
        public async Task<IActionResult> GetAllProductCategoriesAsync()
        {
            APIMessage response = await _productCategoriesServices.GetAllProductCategoriesAsync();

            return StatusCode((int)response.StatusCode, response.ContentObj);
        }

        [HttpGet("{id}")]
        [SwaggerOperation("Returns a product category by id")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(GetProductCategoriesByIdResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product category not found", typeof(string))]
        public async Task<IActionResult> GetProductCategoryByIdAsync(int id)
        {
            APIMessage response = await _productCategoriesServices.GetProductCategoryByIdAsync(id);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return StatusCode((int)response.StatusCode, response.Content);
            }

            return StatusCode((int)response.StatusCode, response.ContentObj);
        }

        [HttpPost]
        [SwaggerOperation("Add a new product category")]
        [SwaggerResponse(StatusCodes.Status200OK, "Category created successfully", typeof(string))]
        public IActionResult AddNewProductCategoryAsync(AddNewProductCategoryRequest request)
        {
            if (!ModelState.IsValid) return CustomReturn(ModelState);

            APIMessage response = _productCategoriesServices.AddNewProductCategoryAsync(request, UserName);

            return StatusCode((int)response.StatusCode, response.Content);
        }

        [HttpPut("{id}")]
        [SwaggerOperation("Updates a product category")]
        [SwaggerResponse(StatusCodes.Status200OK, "Category updated successfully", typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Category not found", typeof(string))]
        public async Task<IActionResult> UpdateProductCategoryAsync(UpdateProductCategoryRequest request, int id)
        {
            APIMessage response = await _productCategoriesServices.UpdateProductCategoryAsync(request, id);

            return StatusCode((int)response.StatusCode, response.Content);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation("Deletes a product Category")]
        [SwaggerResponse(StatusCodes.Status200OK, "Category deleted successfully", typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Category not found", typeof(string))]
        public async Task<IActionResult> DeleteProductCategoryAsync(int id)
        {
            APIMessage response = await _productCategoriesServices.DeleteProductCategoryAsync(id);

            return StatusCode((int)response.StatusCode, response.Content);
        }
    }
}
