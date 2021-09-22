using GerenciamentoComercio_Domain.DTOs;
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
    public class ClientTransactionsServices : IClientTransactionsServices
    {
        private readonly IClientRepository _clientRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IProductRepository _productRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IClientTransactionRepository _clientTransactionRepository;
        private readonly IClientTransactionProductRepository _clientTransactionProductRepository;
        private readonly IClientTransactionServiceRepository _clientTransactionServiceRepository;
        private readonly IProductHistoricRepository _productHistoricRepository;
        private readonly IServiceHistoricRepository _serviceHistoricRepository;
        private readonly ITransactionsCommonServices _transactionsCommonServices;
        private readonly IUnitOfWork _unitOfWork;

        public ClientTransactionsServices(IClientRepository clientRepository,
            IEmployeeRepository employeeRepository,
            IProductRepository productRepository,
            IServiceRepository serviceRepository,
            IUnitOfWork unitOfWork,
            IClientTransactionRepository clientTransactionRepository,
            IClientTransactionProductRepository clientTransactionProductRepository,
            IClientTransactionServiceRepository clientTransactionServiceRepository,
            IProductHistoricRepository productHistoricRepository,
            IServiceHistoricRepository serviceHistoricRepository, 
            ITransactionsCommonServices transactionsCommonServices)
        {
            _clientRepository = clientRepository;
            _employeeRepository = employeeRepository;
            _productRepository = productRepository;
            _serviceRepository = serviceRepository;
            _unitOfWork = unitOfWork;
            _clientTransactionRepository = clientTransactionRepository;
            _clientTransactionProductRepository = clientTransactionProductRepository;
            _clientTransactionServiceRepository = clientTransactionServiceRepository;
            _productHistoricRepository = productHistoricRepository;
            _serviceHistoricRepository = serviceHistoricRepository;
            _transactionsCommonServices = transactionsCommonServices;
        }

        public APIMessage GetTransactionsByProduct(int productCode)
        {
            Product product = _productRepository.GetById(productCode);

            if (product == null)
            {
                return new APIMessage(HttpStatusCode.NotFound,
                    new List<string> { "Produto não encontrado." });
            }

            List<ClientTransactionProduct> transactions =  _clientTransactionProductRepository
                .GetTransactionByProduct(productCode);

            return new APIMessage(HttpStatusCode.OK, transactions
                .Select(x => new GetProductTransactionsResponse
                {
                    Id = x.Id,
                    ClientTransactionId = x.IdClientTransaction.Value,
                    CreationDate = x.CreationDate.Value,
                    CreationUser = x.CreationUser,
                    Price = x.Price ?? 0,
                    ProductId = x.IdProduct.Value,
                    Quantity = x.Quantity.Value
                }));
        }

        public APIMessage GetTransactionsByService(int serviceCode)
        {
            Service service = _serviceRepository.GetById(serviceCode);

            if (service == null)
            {
                return new APIMessage(HttpStatusCode.NotFound,
                    new List<string> { "Serviço não encontrad." });
            }

            List<ClientTransactionService> transactions = _clientTransactionServiceRepository
                .GetTransactionByService(serviceCode);

            return new APIMessage(HttpStatusCode.OK, transactions
                .Select(x => new GetServiceTransactionsResponse
                {
                    Id = x.Id,
                    ClientTransactionId = x.IdClientTransaction.Value,
                    CreationDate = x.CreationDate.Value,
                    CreationUser = x.CreationUser,
                    Price = x.Price ?? 0,
                    ServiceId = x.IdService.Value,
                    Quantity = x.Quantity.Value
                }));
        }

        public APIMessage GetTransactionsByClient(int clientCode)
        {
            Task<Client> client = _clientRepository.GetById(clientCode);

            if (client == null)
            {
                return new APIMessage(HttpStatusCode.NotFound,
                    new List<string> { "Cliente não encontrado." });
            }

            List<ClientTransaction> transactions = _clientTransactionRepository
                .GetTransactionByClient(clientCode);

            return new APIMessage(HttpStatusCode.OK, transactions
                .Select(x => new GetClientTransactionsResponse
                {
                    ClientId = x.IdClient.Value,
                    DiscountPercentage = (int)(x.DiscountPercentage ?? 0), 
                    DiscountPrice = x.DiscountPrice ?? 0,
                    EmployeeId = x.IdEmployee.Value,
                    Observations = x.Observations,
                    TotalPrice = x.TotalPrice.Value,
                    CreationDate = x.CreationDate.Value,
                    CreationUser = x.CreationUser,
                }));
        }

        public APIMessage GetTransacationsByEmployee(int employeeCode)
        {
            Task<Employee> employee = _employeeRepository.GetById(employeeCode);

            if (employee == null)
            {
                return new APIMessage(HttpStatusCode.NotFound,
                    new List<string> { "Funcionário não encontrado." });
            }

            List<ClientTransaction> transactions = _clientTransactionRepository
                .GetTransactionByEmployee(employeeCode);

            return new APIMessage(HttpStatusCode.OK, transactions
                .Select(x => new GetClientTransactionsResponse
                {
                    ClientId = x.IdClient.Value,
                    DiscountPercentage = (int)(x.DiscountPercentage ?? 0),
                    DiscountPrice = x.DiscountPrice ?? 0,
                    EmployeeId = x.IdEmployee.Value,
                    Observations = x.Observations,
                    TotalPrice = x.TotalPrice.Value,
                    CreationDate = x.CreationDate.Value,
                    CreationUser = x.CreationUser,
                }));
        }

        public APIMessage AddNewClientTransactionAsync(AddNewClientTransactionRequest request, string userName)
        {
            var validation = ValidateAddNewClientTransactionAsync(request);

            var prices = new List<decimal>();

            if (validation.Result.StatusCode != HttpStatusCode.OK)
            {
                return new APIMessage(validation.Result.StatusCode, validation.Result.Content);
            }

            var newTransaction = new ClientTransaction
            {
                DiscountPercentage = request.DiscountPercentage,
                DiscountPrice = request.DiscountPrice,
                IdClient = request.ClientId,
                IdEmployee = request.EmployeeId,
                TotalPrice = 0,
                Observations = request.Observations,
                CreationDate = DateTime.Now,
                CreationUser = userName,
            };

            _clientTransactionRepository.AddNew(newTransaction);

            _unitOfWork.Commit();

            foreach (AddNewProductServiceTransactionRequest product in request.Products)
            {
                APIMessage newProduct = _transactionsCommonServices
                    .AddNewProductTransactionAsync(product, userName, newTransaction.Id);

                prices.Add((decimal)newProduct.ContentObj);
            }

            foreach (AddNewProductServiceTransactionRequest service in request.Services)
            {
                APIMessage newService = _transactionsCommonServices
                    .AddNewServiceTransaction(service, userName, newTransaction.Id);

                prices.Add((decimal)newService.ContentObj);
            }

            APIMessage totalPrice = CalculateTransactionTotalPrice(prices.Sum(),
                request.DiscountPrice, request.DiscountPercentage);

            newTransaction.TotalPrice = (decimal)totalPrice.ContentObj;

            _unitOfWork.Commit();

            return new APIMessage(HttpStatusCode.OK,
                new List<string> { "Transação cadastrada com sucesso." });
        }

        public async Task<APIMessage> DeleteClientTransactionAsync(int clientTransactionId)
        {
            ClientTransaction transaction = await _clientTransactionRepository
                .GetById(clientTransactionId);

            if (transaction == null)
            {
                return new APIMessage(HttpStatusCode.NotFound,
                    new List<string> { "Transação não encontrada." });
            }

            List<ClientTransactionProduct> productTransactions = _clientTransactionProductRepository
                .GetTransactionByClientTransaction(clientTransactionId);

            foreach (ClientTransactionProduct productTransaction in productTransactions)
            {
                _clientTransactionProductRepository.Delete(productTransaction);
            }

            List<ClientTransactionService> serviceTransactions = _clientTransactionServiceRepository
                .GetTransactionByClientTransaction(clientTransactionId);

            foreach (ClientTransactionService serviceTransaction in serviceTransactions)
            {
                _clientTransactionServiceRepository.Delete(serviceTransaction);
            }

            _clientTransactionRepository.Delete(transaction);

            _unitOfWork.Commit();

            return new APIMessage(HttpStatusCode.OK,
                new List<string> { "Transação excluída com sucesso." });
        }

        private async Task<APIMessage> ValidateAddNewClientTransactionAsync(AddNewClientTransactionRequest request)
        {
            Client client = await _clientRepository.GetById(request.ClientId);

            if (client == null)
            {
                return new APIMessage(HttpStatusCode.NotFound,
                    new List<string> { "Cliente não encontrado." });
            }

            Employee employee = await _employeeRepository.GetById(request.EmployeeId);

            if (employee == null)
            {
                return new APIMessage(HttpStatusCode.NotFound,
                    new List<string> { "Funcionário não encontrado." });
            }

            foreach (AddNewProductServiceTransactionRequest clientProduct in request.Products)
            {
                Product product = _productRepository.GetById(clientProduct.Id);

                if (product == null)
                {
                    return new APIMessage(HttpStatusCode.NotFound,
                        new List<string> { $"Produto {clientProduct.Id} não encontrado." });
                }
            }

            foreach (AddNewProductServiceTransactionRequest clientService in request.Services)
            {
                Service service = _serviceRepository.GetById(clientService.Id);

                if (service == null)
                {
                    return new APIMessage(HttpStatusCode.NotFound,
                        new List<string> { $"Serviço {clientService.Id} não encontrado." });
                }
            }

            return new APIMessage(HttpStatusCode.OK, "");
        }

        private APIMessage CalculateTransactionTotalPrice(decimal price, int discountPrice, int discountPercentage)
        {
            if (discountPrice != 0)
            {
                price -= discountPrice;
            }
            else if (discountPercentage != 0)
            {
                price = price * 100 / discountPercentage;
            }

            return new APIMessage(HttpStatusCode.OK, price);
        }
    }
}
