using GerenciamentoComercio_Domain.DTOs.Products;
using GerenciamentoComercio_Domain.Utils.APIMessage;
using GerenciamentoComercio_Domain.Utils.UnitOfWork;
using GerenciamentoComercio_Domain.v1.Interfaces.Repositories;
using GerenciamentoComercio_Domain.v1.Interfaces.Services;
using GerenciamentoComercio_Infra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GerenciamentoComercio_Domain.v1.Services
{
    public class ProductsServices : IProductsServices
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductsServices(IProductRepository productRepository,
            IProductCategoryRepository productCategoryRepository,
            IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public APIMessage GetAllProducts()
        {
            IEnumerable<Product> products = _productRepository.GetMany();

            return new APIMessage(HttpStatusCode.OK, products
                .Select(x => new GetAllProductsResponse
                {
                    Name = x.Name,
                    CategoryId = x.IdProductCategory ?? 0,
                    Description = x.Description,
                    CategoryName = x.IdProductCategoryNavigation == null ? null : x.IdProductCategoryNavigation.Description,
                    Id = x.Id
                }));
        }

        public APIMessage GetProductById(int id)
        {
            Product product = _productRepository.GetById(id);

            if (product == null)
            {
                return new APIMessage(HttpStatusCode.NotFound,
                    new List<string> { "Produto não encontrado." });
            }

            return new APIMessage(HttpStatusCode.OK, new GetProductByIdResponse
            {
                Name = product.Name,
                CategoryId = product.IdProductCategory ?? 0,
                CategoryName = product.IdProductCategoryNavigation == null ? null : product.IdProductCategoryNavigation.Description,
                Description = product.Description
            });
        }

        public async Task<APIMessage> AddNewProductAsync(AddNewProductRequest request, string userName)
        {
            ProductCategory category = await _productCategoryRepository
                .GetById(request.CategoryId ?? 0);

            if (category == null && request.CategoryId != null)
            {
                return new APIMessage(HttpStatusCode.NotFound,
                    new List<string> { "Categoria de produto não encontrada." });
            }

            var newProduct = new Product
            {
                Description = request.Description,
                IsActive = true,
                Name = request.Name,
                IdProductCategory = request.CategoryId,
                CreationDate = DateTime.Now,
                CreationUser = userName
            };

            _productRepository.AddNew(newProduct);

            _unitOfWork.Commit();

            return new APIMessage(HttpStatusCode.OK, new List<string> { "Produto cadastrado com sucesso." });
        }

        public async Task<APIMessage> UpdateProductAsync(UpdateProductRequest request, int id)
        {
            ProductCategory category = await _productCategoryRepository
                .GetById(request.CategoryId ?? 0);

            if (category == null && request.CategoryId != null)
            {
                return new APIMessage(HttpStatusCode.NotFound,
                    new List<string> { "Categoria de produto não encontrada." });
            }

            Product product = _productRepository.GetById(id);

            if (product == null)
            {
                return new APIMessage(HttpStatusCode.NotFound,
                    new List<string> { "Produto não encontrado." });
            }

            product.Name = request.Name ?? product.Name;
            product.Description = request.Description ?? product.Description;
            product.IdProductCategory = request.CategoryId ?? product.IdProductCategory;
            product.IsActive = request.IsActive ?? product.IsActive;

            _productRepository.Update(product);

            _unitOfWork.Commit();

            return new APIMessage(HttpStatusCode.OK, new List<string> { "Produto atualizado com sucesso." });
        }

        public APIMessage DeleteProduct(int id)
        {
            Product product =  _productRepository.GetById(id);

            if (product == null)
            {
                return new APIMessage(HttpStatusCode.NotFound,
                    new List<string> { "Produto não encontrado." });
            }

            _productRepository.Delete(product);

            _unitOfWork.Commit();

            return new APIMessage(HttpStatusCode.OK, new List<string> { "Produto excluído com sucesso." });
        }
    }
}
