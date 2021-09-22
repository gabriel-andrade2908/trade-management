using GerenciamentoComercio_Domain.DTOs.ProductCategories;
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
    public class ProductCategoriesServices : IProductCategoriesServices
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductCategoriesServices(IProductCategoryRepository productCategoryRepository,
            IUnitOfWork unitOfWork,
            IProductRepository productRepository)
        {
            _productCategoryRepository = productCategoryRepository;
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }

        public async Task<APIMessage> GetAllProductCategoriesAsync()
        {
            IEnumerable<ProductCategory> productCategories = await _productCategoryRepository.GetMany();

            return new APIMessage(HttpStatusCode.OK, productCategories
                .Select(x => new GetAllProductCategoriesResponse
                {
                    Description = x.Description,
                    Id = x.Id,
                    IsActive = x.IsActive,
                    Title = x.Title
                }));
        }

        public async Task<APIMessage> GetProductCategoryByIdAsync(int id)
        {
            ProductCategory productCategory = await _productCategoryRepository.GetById(id);

            if (productCategory == null)
            {
                return new APIMessage(HttpStatusCode.NotFound,
                    new List<string> { "Categoria não encontrada." });
            }

            return new APIMessage(HttpStatusCode.OK, new GetProductCategoriesByIdResponse
            {
                Description = productCategory.Description,
                Title = productCategory.Title,
                IsActive = productCategory.IsActive
            });
        }

        public APIMessage AddNewProductCategoryAsync(AddNewProductCategoryRequest request, string userName)
        {
            ProductCategory category = _productCategoryRepository.GetCategoryByTitle(request.Title);

            if (category != null)
            {
                return new APIMessage(HttpStatusCode.BadRequest,
                    new List<string> { "Já existe uma categoria com o mesmo título." });
            }

            var newProductCategory = new ProductCategory
            {
                Description = request.Description,
                IsActive = true,
                Title = request.Title,
                CreationDate = DateTime.Now,
                CreationUser = userName
            };

            _productCategoryRepository.AddNew(newProductCategory);

            _unitOfWork.Commit();

            return new APIMessage(HttpStatusCode.OK, new List<string> { "Categoria cadastrada com sucesso." });
        }

        public async Task<APIMessage> UpdateProductCategoryAsync(UpdateProductCategoryRequest request, int id)
        {
            ProductCategory productCategory = await _productCategoryRepository.GetById(id);

            if (productCategory == null)
            {
                return new APIMessage(HttpStatusCode.NotFound,
                    new List<string> { "Categoria não encontrada." });
            }

            ProductCategory category = _productCategoryRepository.GetCategoryByTitle(request.Title);

            if (category != null && id != category.Product.FirstOrDefault(x => x.Id == id).Id)
            {
                return new APIMessage(HttpStatusCode.BadRequest,
                    new List<string> { "Já existe uma categoria com o mesmo título." });
            }

            productCategory.Title = request.Title ?? productCategory.Title;
            productCategory.Description = request.Description ?? productCategory.Description;
            productCategory.IsActive = request.IsActive ?? productCategory.IsActive;

            _productCategoryRepository.Update(productCategory);

            _unitOfWork.Commit();

            return new APIMessage(HttpStatusCode.OK, new List<string> { "Categoria atualizada com sucesso." });
        }

        public async Task<APIMessage> DeleteProductCategoryAsync(int id)
        {
            ProductCategory productCategory = await _productCategoryRepository.GetById(id);

            if (productCategory == null)
            {
                return new APIMessage(HttpStatusCode.NotFound,
                    new List<string> { "Categoria não encontrado." });
            }

            List<Product> products = _productRepository.GetProductByCategory(id);

            foreach (var product in products)
            {
                UpdateProductCategory(product);
            }

            _productCategoryRepository.Delete(productCategory);

            _unitOfWork.Commit();

            return new APIMessage(HttpStatusCode.OK, new List<string> { "Categoria excluída com sucesso." });
        }

        private void UpdateProductCategory(Product product)
        {
            product.IdProductCategory = null;
        }
    }
}
