using GerenciamentoComercio_Domain.DTOs.ServiceCategories;
using GerenciamentoComercio_Domain.Utils.APIMessage;
using GerenciamentoComercio_Domain.Utils.UnitOfWork;
using GerenciamentoComercio_Domain.v1.Interfaces.Repositories;
using GerenciamentoComercio_Domain.v1.Interfaces.Services;
using GerenciamentoComercio_Infra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GerenciamentoComercio_Domain.v1.Services
{
    public class ServiceCategoriesServices : IServiceCategoriesServices
    {
        private readonly IServiceCategoryRepository _serviceCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ServiceCategoriesServices(IServiceCategoryRepository serviceCategoryRepository,
            IUnitOfWork unitOfWork)
        {
            _serviceCategoryRepository = serviceCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<APIMessage> GetAllServiceCategoriesAsync()
        {
            IEnumerable<ServiceCategory> categories = await _serviceCategoryRepository.GetMany();

            return new APIMessage(HttpStatusCode.OK, categories
                .Select(x => new GetAllServiceCategoriesResponse
                {
                    Description = x.Description,
                    Id = x.Id,
                    IsActive = x.IsActive ?? false,
                    Title = x.Title
                }));
        }

        public async Task<APIMessage> GetServiceCategoryByIdAsync(int id)
        {
            ServiceCategory category = await _serviceCategoryRepository.GetById(id);

            if (category == null)
            {
                return new APIMessage(HttpStatusCode.NotFound,
                    new List<string> { "Categoria não encontrada." });
            }

            return new APIMessage(HttpStatusCode.OK, new GetServiceCategoryByIdResponse
            {
                Description = category.Description,
                Title = category.Title,
                IsActive = category.IsActive ?? false
            });
        }

        public APIMessage AddNewServiceCategoryAsync(AddNewServiceCategoryRequest request, string userName)
        {
            ServiceCategory category = _serviceCategoryRepository.GetCategoryByTitle(request.Title);

            if (category != null)
            {
                return new APIMessage(HttpStatusCode.NotFound,
                    new List<string> { "Já existe uma categoria com o mesmo título." });
            }

            var newServiceCategory = new ServiceCategory
            {
                Description = request.Description,
                IsActive = true,
                Title = request.Title,
                CreationDate = DateTime.Now,
                CreationUser = userName
            };

            _serviceCategoryRepository.AddNew(newServiceCategory);

            _unitOfWork.Commit();

            return new APIMessage(HttpStatusCode.OK, new List<string> { "Categoria cadastrada com sucesso." });
        }

        public async Task<APIMessage> UpdateServiceCategoryAsync(UpdateServiceCategoryRequest request, int id)
        {
            ServiceCategory serviceCategory = await _serviceCategoryRepository.GetById(id);

            if (serviceCategory == null)
            {
                return new APIMessage(HttpStatusCode.NotFound,
                    new List<string> { "Categoria não encontrada." });
            }

            ServiceCategory category = _serviceCategoryRepository.GetCategoryByTitle(request.Title);

            if (category != null && category.Id != id )
            {
                return new APIMessage(HttpStatusCode.NotFound,
                    new List<string> { "Já existe uma categoria com o mesmo título." });
            }

            serviceCategory.Title = request.Title ?? serviceCategory.Title;
            serviceCategory.Description = request.Description ?? serviceCategory.Description;
            serviceCategory.IsActive = request.IsActive ?? serviceCategory.IsActive;

            _serviceCategoryRepository.Update(serviceCategory);

            _unitOfWork.Commit();

            return new APIMessage(HttpStatusCode.OK, new List<string> { "Categoria atualizada com sucesso." });
        }

        public async Task<APIMessage> DeleteServiceCategoryAsync(int id)
        {
            ServiceCategory serviceCategory = await _serviceCategoryRepository.GetById(id);

            if (serviceCategory == null)
            {
                return new APIMessage(HttpStatusCode.NotFound,
                    new List<string> { "Categoria não encontrado." });
            }

            _serviceCategoryRepository.Delete(serviceCategory);

            _unitOfWork.Commit();

            return new APIMessage(HttpStatusCode.OK, new List<string> { "Categoria excluída com sucesso." });
        }
    }
}
