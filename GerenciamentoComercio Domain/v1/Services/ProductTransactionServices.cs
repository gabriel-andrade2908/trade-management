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
    public class ProductTransactionServices : IProductTransactionServices
    {
        private readonly IClientTransactionProductRepository _clientTransactionProductRepository;
        private readonly IClientTransactionRepository _clientTransactionRepository;
        private readonly IProductRepository _productRepository;
        private readonly ITransactionsCommonServices _transactionsCommonServices;
        private readonly IProductHistoricRepository _productHistoricRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductTransactionServices(IClientTransactionProductRepository clientTransactionProductRepository,
            IProductRepository productRepository,
            IUnitOfWork unitOfWork,
            IClientTransactionRepository clientTransactionRepository,
            ITransactionsCommonServices transactionsCommonServices, 
            IProductHistoricRepository productHistoricRepository)
        {
            _clientTransactionProductRepository = clientTransactionProductRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _clientTransactionRepository = clientTransactionRepository;
            _transactionsCommonServices = transactionsCommonServices;
            _productHistoricRepository = productHistoricRepository;
        }

        public async Task<APIMessage> AddNewProductTransactionAsync(List<AddNewProductServiceTransactionRequest> products, int clientTransactionId, string userName)
        {
            ClientTransaction clientTransaction = await _clientTransactionRepository
                .GetById(clientTransactionId);

            if (clientTransaction == null)
            {
                return new APIMessage(HttpStatusCode.NotFound,
                    new List<string> { "A transação informada não existe." });
            }

            var pricesToChangeInClientTransaction = new List<decimal>();

            foreach (AddNewProductServiceTransactionRequest productRequest in products)
            {
                Product product = _productRepository.GetById(productRequest.Id);

                if (product == null)
                {
                    return new APIMessage(HttpStatusCode.NotFound,
                        new List<string> { $"Produto {productRequest.Id} não foi encontrado." });
                }

                if (_clientTransactionRepository.CheckIfProductAlreadyExistInTransaction(productRequest.Id))
                {
                    return new APIMessage(HttpStatusCode.NotFound,
                        new List<string> { $"O produto {productRequest.Id} já está inserido na transação." });
                }

                APIMessage newProductTransaction = _transactionsCommonServices
                    .AddNewProductTransactionAsync(productRequest, userName, clientTransactionId);

                pricesToChangeInClientTransaction.Add((decimal)newProductTransaction.ContentObj);
            }

            UpdateClientTransaction(clientTransaction,
                pricesToChangeInClientTransaction.Sum(), true);

            _unitOfWork.Commit();

            return new APIMessage(HttpStatusCode.OK,
                new List<string> { "Transação cadastrada com sucesso." });
        }

        public async Task<APIMessage> UpdateProductTransactionAsync(int quantity, int id)
        {
            ClientTransactionProduct productTransaction = await _clientTransactionProductRepository
                .GetById(id);

            if (productTransaction == null)
            {
                return new APIMessage(HttpStatusCode.NotFound,
                new List<string> { "Transação não encontrada." });
            }

            ClientTransaction clientTransaction = await _clientTransactionRepository
                .GetById(productTransaction.IdClientTransaction.Value);

            decimal priceToChange = (productTransaction.Quantity.Value - quantity) *
                productTransaction.Price.Value;

            bool isAdd = quantity > productTransaction.Quantity;

            UpdateClientTransaction(clientTransaction, priceToChange, isAdd);

            ProductHistoric productHistoric = _productHistoricRepository
                .GetHistoricByProductId(productTransaction.IdProduct.Value)
                .LastOrDefault();

            int quantityToChange = quantity - productTransaction.Quantity.Value ;

            bool isAddQuantity = productTransaction.Quantity > quantity;

            UpdateProductQuantity(productHistoric, quantityToChange, isAddQuantity);

            productTransaction.Quantity = quantity;

            _unitOfWork.Commit();

            return new APIMessage(HttpStatusCode.OK,
                new List<string> { "Transação de produto atualizada com sucesso." });
        }

        public async Task<APIMessage> DeleteProductTransactionAsync(int id)
        {
            ClientTransactionProduct productTransaction = await _clientTransactionProductRepository
                .GetById(id);

            if (productTransaction == null)
            {
                return new APIMessage(HttpStatusCode.NotFound,
                    new List<string> { "Transação de produto não encontrada." });
            }

            _clientTransactionProductRepository.Delete(productTransaction);

            ClientTransaction clientTransaction = await _clientTransactionRepository
                .GetById(productTransaction.IdClientTransaction.Value);

            decimal pricesToChangeInClientTransaction = productTransaction.Price.Value *
                productTransaction.Quantity.Value;

            UpdateClientTransaction(clientTransaction,
                pricesToChangeInClientTransaction, false);

            ProductHistoric productHistoric = _productHistoricRepository
                .GetHistoricByProductId(productTransaction.IdProduct.Value)
                .LastOrDefault();

            UpdateProductQuantity(productHistoric, productTransaction.Quantity.Value, false);

            _unitOfWork.Commit();

            return new APIMessage(HttpStatusCode.OK,
                new List<string> { "Transação de produto excluída com sucesso." });
        }

        private static void UpdateClientTransaction(ClientTransaction clientTransaction, decimal priceToChange, bool addValue)
        {
            if (addValue)
            {
                clientTransaction.TotalPrice += priceToChange;
            }
            else
            {
                clientTransaction.TotalPrice -= priceToChange;
            }
        }

        private static void UpdateProductQuantity(ProductHistoric productHistoric, int quantityToChange, bool addValue)
        {
            if (addValue)
            {
                productHistoric.Quantity += quantityToChange;
            }
            else
            {
                productHistoric.Quantity -= quantityToChange;
            }
        }
    }
}
