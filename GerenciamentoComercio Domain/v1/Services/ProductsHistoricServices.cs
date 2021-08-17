using GerenciamentoComercio_Domain.DTOs.ProductHistoric;
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
    public class ProductsHistoricServices : IProductsHistoricServices
    {
        private readonly IProductHistoricRepository _productHistoricRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductsHistoricServices(IUnitOfWork unitOfWork,
            IProductHistoricRepository productHistoricRepository)
        {
            _unitOfWork = unitOfWork;
            _productHistoricRepository = productHistoricRepository;
        }

        public async Task<APIMessage> GetAllProductsHistoricAsync()
        {
            IEnumerable<ServiceHistoric> historics = await _productHistoricRepository.GetMany();

            return new APIMessage(HttpStatusCode.OK, historics
                .Select(x => new GetAllProductsHistoricResponse
                {
                    Id = x.Id,
                    CreationDate = x.CreationDate.Value,
                    CreationUser = x.CreationUser,
                    ProductId = x.IdProduct ?? 0,
                    ProductPrice = x.Price,
                    ProductQuantity = x.Quantity ?? 0
                }));
        }

        public async Task<APIMessage> GetProductHistoricByIdAsync(int id)
        {
            ServiceHistoric historic = await _productHistoricRepository.GetById(id);

            if (historic == null)
            {
                return new APIMessage(HttpStatusCode.NotFound,
                    new List<string> { "Histórico não encontrada." });
            }

            return new APIMessage(HttpStatusCode.OK, new GetProductHistoricByIdResponse
            {
                CreationDate = historic.CreationDate.Value,
                CreationUser = historic.CreationUser,
                ProductId = historic.IdProduct ?? 0,
                ProductPrice = historic.Price,
                ProductQuantity = historic.Quantity ?? 0,
            });
        }

        public APIMessage GetHistoricByProductAsync(int productId)
        {
            List<ServiceHistoric> productHistoric = _productHistoricRepository
                .GetHistoricByProductId(productId);

            return new APIMessage(HttpStatusCode.OK, productHistoric
                .Select(x => new GetProductHistoricByProductResponse
                {
                    CreationDate = x.CreationDate.Value,
                    CreationUser = x.CreationUser,
                    ProductPrice = x.Price,
                    ProductQuantity = x.Quantity ?? 0,
                    Id = x.Id
                }));
        }

        public APIMessage AddNewProductHistoricAsync(AddNewProductHistoricRequest request, string userName)
        {
            var newProductHistoric = new ServiceHistoric
            {
                IdProduct = request.ProductId,
                Price = request.ProductPrice,
                Quantity = request.ProductQuantity,
                CreationDate = DateTime.Now,
                CreationUser = userName
            };

            _productHistoricRepository.AddNew(newProductHistoric);

            _unitOfWork.Commit();

            return new APIMessage(HttpStatusCode.OK, new List<string> { "Histórico cadastrado com sucesso." });
        }

        public async Task<APIMessage> UpdateProductHistoricAsync(UpdateProductHistoricRequest request, int id)
        {
            ServiceHistoric productHistoric = await _productHistoricRepository.GetById(id);

            if (productHistoric == null)
            {
                return new APIMessage(HttpStatusCode.NotFound,
                    new List<string> { "Histórico não encontrado." });
            }

            productHistoric.Price = request.ProductPrice ?? productHistoric.Price;
            productHistoric.Quantity = request.ProductQuantity ?? productHistoric.Quantity;

            _productHistoricRepository.Update(productHistoric);

            _unitOfWork.Commit();

            return new APIMessage(HttpStatusCode.OK, new List<string> { "Histórico atualizado com sucesso." });
        }

        public async Task<APIMessage> DeleteProductHistoricAsync(int id)
        {
            ServiceHistoric productHistoric = await _productHistoricRepository.GetById(id);

            if (productHistoric == null)
            {
                return new APIMessage(HttpStatusCode.NotFound,
                    new List<string> { "Histórico não encontrado." });
            }

            _productHistoricRepository.Delete(productHistoric);

            _unitOfWork.Commit();

            return new APIMessage(HttpStatusCode.OK, new List<string> { "Histórico excluído com sucesso." });
        }
    }
}

