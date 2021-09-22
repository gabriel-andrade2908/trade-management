using GerenciamentoComercio_Domain.DTOs.ProductTransaction;
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
    public class TransactionsCommonServices : ITransactionsCommonServices
    {
        private readonly IProductHistoricRepository _productHistoricRepository;
        private readonly IClientTransactionServiceRepository _clientTransactionServiceRepository;
        private readonly IClientTransactionProductRepository _clientTransactionProductRepository;
        private readonly IServiceHistoricRepository _serviceHistoricRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionsCommonServices(IProductHistoricRepository productHistoricRepository,
            IClientTransactionServiceRepository clientTransactionServiceRepository,
            IServiceHistoricRepository serviceHistoricRepository,
            IUnitOfWork unitOfWork,
            IClientTransactionProductRepository clientTransactionProductRepository)
        {
            _productHistoricRepository = productHistoricRepository;
            _clientTransactionServiceRepository = clientTransactionServiceRepository;
            _serviceHistoricRepository = serviceHistoricRepository;
            _unitOfWork = unitOfWork;
            _clientTransactionProductRepository = clientTransactionProductRepository;
        }

        public APIMessage AddNewProductTransactionAsync(AddNewProductServiceTransactionRequest request, string userName, int clientTransactionId)
        {
            var product = _productHistoricRepository
                .GetHistoricByProductId(request.Id)
                .LastOrDefault();

            if (product.Quantity < request.Quantity)
            {
                return new APIMessage(HttpStatusCode.NotFound,
                    new List<string> { "A quantidade informada do produto é maior do que a quantidade disponível." });
            }

            var newProductTransaction = new ClientTransactionProduct
            {
                IdProduct = request.Id,
                Price = product.Price,
                Quantity = request.Quantity,
                CreationDate = DateTime.Now,
                CreationUser = userName,
                IdClientTransaction = clientTransactionId
            };

            _clientTransactionProductRepository.AddNew(newProductTransaction);

            product.Quantity -= request.Quantity;

            _unitOfWork.Commit();

            return new APIMessage(HttpStatusCode.OK, product.Price * request.Quantity);
        }

        public APIMessage AddNewServiceTransaction(AddNewProductServiceTransactionRequest request, string userName, int clientTransactionId)
        {
            decimal? servicePrice = _serviceHistoricRepository
                .GetHistoricByServiceId(request.Id)
                .LastOrDefault()
                .Price;

            var newServiceTransaction = new ClientTransactionService
            {
                Price = servicePrice,
                Quantity = request.Quantity,
                CreationDate = DateTime.Now,
                CreationUser = userName,
                IdClientTransaction = clientTransactionId,
                IdService = request.Id
            };

            _clientTransactionServiceRepository.AddNew(newServiceTransaction);

            _unitOfWork.Commit();

            return new APIMessage(HttpStatusCode.OK, servicePrice * request.Quantity);
        }
    }
}
