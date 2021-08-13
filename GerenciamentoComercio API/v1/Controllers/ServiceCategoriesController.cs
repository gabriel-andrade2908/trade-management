using GerenciamentoComercio_Domain.DTOs.ServiceCategories;
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
    [Route("v{version:apiVersion}/service-categories/")]
    [ApiVersion("1.0")]
    public class ServiceCategoriesController : MainController
    {
        private readonly IServiceCategoriesServices _servicesCategoriesServices;
        public ServiceCategoriesController(IUserApp userApp,
             INotifier notifier,
             IServiceCategoriesServices serviceCategoriesServices) : base(userApp, notifier)
        {
            _servicesCategoriesServices = serviceCategoriesServices;
        }

        [HttpGet]
        [SwaggerOperation("Returns all service categories")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(List<GetAllServiceCategoriesResponse>))]
        public async Task<IActionResult> GetAllServiceCategoriesAsync()
        {
            APIMessage response = await _servicesCategoriesServices.GetAllServiceCategoriesAsync();

            return StatusCode((int)response.StatusCode, response.ContentObj);
        }

        [HttpGet("{id}")]
        [SwaggerOperation("Returns a service category by id")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(GetServiceCategoryByIdResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Service category not found", typeof(string))]
        public async Task<IActionResult> GetServiceCategoryByIdAsync(int id)
        {
            APIMessage response = await _servicesCategoriesServices.GetServiceCategoryByIdAsync(id);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return StatusCode((int)response.StatusCode, response.Content);
            }

            return StatusCode((int)response.StatusCode, response.ContentObj);
        }

        [HttpPost]
        [SwaggerOperation("Add a new service category")]
        [SwaggerResponse(StatusCodes.Status200OK, "Category created successfully", typeof(string))]
        public IActionResult AddNewServiceCategoryAsync(AddNewServiceCategoryRequest request)
        {
            if (!ModelState.IsValid) return CustomReturn(ModelState);

            APIMessage response = _servicesCategoriesServices.AddNewServiceCategoryAsync(request, UserName);

            return StatusCode((int)response.StatusCode, response.Content);
        }

        [HttpPut("{id}")]
        [SwaggerOperation("Updates a service category")]
        [SwaggerResponse(StatusCodes.Status200OK, "Category updated successfully", typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Category not found", typeof(string))]
        public async Task<IActionResult> UpdateServiceCategoryAsync(UpdateServiceCategoryRequest request, int id)
        {
            APIMessage response = await _servicesCategoriesServices.UpdateServiceCategoryAsync(request, id);

            return StatusCode((int)response.StatusCode, response.Content);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation("Deletes a service Category")]
        [SwaggerResponse(StatusCodes.Status200OK, "Category deleted successfully", typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Category not found", typeof(string))]
        public async Task<IActionResult> DeleteServiceCategoryAsync(int id)
        {
            APIMessage response = await _servicesCategoriesServices.DeleteServiceCategoryAsync(id);

            return StatusCode((int)response.StatusCode, response.Content);
        }
    }
}
